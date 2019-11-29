from flask import jsonify, request, Blueprint
from flask_jwt_extended import jwt_required, get_jwt_identity
from flask import current_app as app

from project.middleware.error_handler import InvalidUsage
from project.service.user_calls import UserProxyAccess


proxy_blueprint = Blueprint('proxy', __name__,)


@proxy_blueprint.route('/', methods=['POST'])
def login_user_and_create_token():
    """
        Endpoint to get a token
        ---
        tags:
          - Token Methods
        parameters:
          - name: body
            in: body
            required: true
            schema:
              type: object
              properties:
                username:
                  type: string
                  example: testadmin
                password:
                  type: string
                  example: admin
                id:
                  type: number
                  example: 1
        responses:
          404:
            description: User was not found
          200:
            description: Your token has been created
    """
    url = app.config["USERS_URL"]
    user_service = UserProxyAccess(url)
    created_token = user_service.verify_user_can_create_token(request.json['username'],
                                                            request.json['password'],
                                                            request.json['id'])
    response = {'access_token': created_token, 'expires_in': 3600 }
    return jsonify(response), 200


@proxy_blueprint.route('/', methods=['GET'])
@jwt_required
def test_tokens():
    """
        Endpoint to showcase token usage
        ---
        tags:
          - Token Methods
        security:
          - APIKeyHeader: []
        responses:
          200:
            description: Your token has been validated
    """
    current_user = get_jwt_identity()
    resp = jsonify({"protected": "{} - you saw me!".format(current_user['username'])})
    resp.status_code = 200

    return resp


@proxy_blueprint.route('/admin', methods=['GET'])
@jwt_required
def protected():
    """
    Protected content method.
    ---
    description: Protected content method. Can not be seen without valid token.
    tags:
      - Token Methods
    security:
      - APIKeyHeader: []
    responses:
      200:
        description: User successfully accessed the content.
    """
    allowed_role = 'admin'
    current_user_role = get_jwt_identity()['role']
    if current_user_role != allowed_role:
        raise InvalidUsage('Forbidden for this user', status_code=403)
    else:
        return jsonify({"msg": "You are a super cool admin"})



@proxy_blueprint.errorhandler(InvalidUsage)
def handle_invalid_usage(error):
    response = jsonify(error.to_dict())
    response.status_code = error.status_code
    return response
