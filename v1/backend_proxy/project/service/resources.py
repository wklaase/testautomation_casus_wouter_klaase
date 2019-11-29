from flask_jwt_extended import create_access_token, create_refresh_token
from project.middleware.error_handler import InvalidUsage


class UserLogin:

    @staticmethod
    def create_token(user):
        access_token = create_access_token(identity=user)
        #version 2 -> only active users can log in
      #  active = user['active']
      #  if not active:
      #      raise InvalidUsage('Inactive user detected, can not login to system', status_code=400)
        return {
            'access_token': access_token
        }