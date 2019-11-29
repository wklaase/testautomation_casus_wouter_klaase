from pymongo import MongoClient
import glob
import json
import requests
import time

CONNECTION = 'mongodb://moviemongo:27017'


def insert_collections():
    connectionstring = CONNECTION
    client = MongoClient(connectionstring)
    db = client.video
    try:
        print("dropping movies collection")
        db.movies.drop()
    except:
        print("no collection to delete")
    postids = []
    files = get_json_files()
    for file in files:
        if file == "./movies.json":
            postids = insert_specific_collection(file, db.movies)
    return postids


def insert_specific_collection(json_file, col):
    post_ids = []
    with open(json_file) as data_file:
        data = json.load(data_file)
    for d in data:
        print(d)
        try:
            post_id = col.insert_one(d).inserted_id
            post_ids.append(post_id)
        except:
            print("error!")
    return post_ids


def get_json_files():
    path = './*.json'
    files = glob.glob(path)
    return files


def create_sample_users():
    headers = {'content-type': 'application/json'}
    admin_user = json.dumps({"userName": "testadmin",
                            "role": "admin", 
                            "passWord": "admin",
                            "active": True})

    non_admin_user = json.dumps({"userName": "testuser",
                                 "role": "user",
                                 "passWord": "user",
                                 "active": True})

    resp_admin = requests.post("http://users:4242/api/Users", data=admin_user, headers=headers)
    resp_user = requests.post("http://users:4242/api/Users", data=non_admin_user, headers=headers)
    posted_users = [resp_admin.text, resp_user.text]
    return posted_users


print("Starting to import")
jsonfiles = get_json_files()
for file in jsonfiles:
    print("Going to import the following file: " + file)
ids = insert_collections()
print(ids)
print("finished mongo movies")
print("creating users")
for i in range(15, -1, -1):
    print("waiting for: " + str(i) + " seconds")
    time.sleep(1)
users = create_sample_users()
for user in users:
    print("created: " + user)
print("finished postgres users")
print("finished seeding")
