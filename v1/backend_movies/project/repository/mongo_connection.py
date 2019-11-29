from pymongo import MongoClient
import project
import os


class MongoConnection:

    @staticmethod
    def connect_to_mongo():
        app_settings = os.getenv('APP_SETTINGS')
        env = getattr(project.config, app_settings)
        print(env.CONNECTION)
        client = MongoClient(env.CONNECTION)
        db = client.video
        return db