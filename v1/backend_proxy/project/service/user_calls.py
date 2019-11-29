import requests
import json
from project.middleware.error_handler import InvalidUsage
from project.models.users import UserObject
from .resources import UserLogin


class UserProxyAccess(object):

    def __init__(self, url):
        self.url = url

    def verify_user_can_create_token(self, username, password, id):
        self.validate_user_and_id_match(id, username)
        user = self.get_user_role(id)
        post_body = json.dumps({"userName": username.lower(), "passWord": password})
        headers = {'content-type': 'application/json'}
        r = requests.post(self.url + "Validation", data=post_body, headers=headers)
        response = json.loads(r.text)
        if r.status_code is not 200:
            raise InvalidUsage(response['Error'], status_code=r.status_code)
        user_can_login = response['passWordIsValid']
        if not user_can_login:
            raise InvalidUsage('Password is not valid, no action will be taken', status_code=400)
        token = UserLogin.create_token(user)
        return token['access_token']

    def validate_user_and_id_match(self, id, username):
        r = requests.get(self.url + "Users/" + str(id))
        matched_id = r.json()['username'].lower() == username.lower()
        if not matched_id:
            raise InvalidUsage('User and id do not match', status_code=400)
        return

    def get_user_role(self, id):
        r = requests.get(self.url + "Users/" + str(id))
        user_info = json.loads(r.text)
        return user_info

    def get_user_id(self, username):
        r = requests.get(self.url + "Users")
        response = json.loads(r.text)
        user_object = None
        for user in response['allUsers']:
            if user['username'].lower() == username.lower():
                user_object = UserObject(user['username'], user['role'], user['id'], user['active'])
        if user_object is None:
            raise InvalidUsage('No user found for that user_name', status_code=404)
        return user_object

    def get_all_users(self):
        r = requests.get(self.url + "Users")
        return json.loads(r.text)

    def create_a_new_user(self, user):
        headers = {'content-type': 'application/json'}
        r = requests.post(self.url + "Users", data=json.dumps(user.__dict__), headers=headers)
        response = json.loads(r.text)
        if r.status_code is not 201:
            raise InvalidUsage(response['Error'], status_code=r.status_code)
        return response

    def delete_a_user(self, id):
        raise InvalidUsage('Error', status_code=501)

    def update_a_user(self, id, active):
        raise InvalidUsage('Error', status_code=501)
