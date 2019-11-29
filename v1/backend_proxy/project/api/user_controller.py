from flask import jsonify, request, Blueprint
from flask import current_app as app
from flask_jwt_extended import jwt_required, get_jwt_identity
from flask_restplus import inputs

from project.middleware.error_handler import InvalidUsage
from project.service.user_calls import UserProxyAccess
from project.models import users


user_blueprint = Blueprint('user', __name__,)


@user_blueprint.route('/', methods=['POST'])
def create_a_new_user():
    """
    Create a new user in the backend
    ---
    description: Protected content method. Can not be seen without valid token. Only admins can create admins
    tags:
      - User Methods
    parameters:
      - name: body
        in: body
        required: true
        schema:
          type: object
          properties:
            username:
              type: string
              example: newuser
            password:
              type: string
              example: password
    responses:
      200:
        description: User successfully accessed the content.
    """
    user_to_post = users.CreationUserObject(request.json['username'],
                                            request.json['password'],
                                            'user')
    url = app.config["USERS_URL"]
    user_service = UserProxyAccess(url)
    created_user = user_service.create_a_new_user(user_to_post)
    return jsonify(created_user), 200



@user_blueprint.route('/admins', methods=['POST'])
@jwt_required
def create_a_new_admin():
    """
    Create a new admin in the backend
    ---
    description: Protected content method. Can not be seen without valid token. Only admins can create admins
    tags:
      - User Methods
    security:
      - APIKeyHeader: []
    parameters:
      - name: body
        in: body
        required: true
        schema:
          type: object
          properties:
            username:
              type: string
              example: newadmin
            password:
              type: string
              example: password
    responses:
      200:
        description: User successfully accessed the content.
    """
    current_user_role = get_jwt_identity()['role']
    if current_user_role == 'admin':
        # admin
        user_to_post = users.CreationUserObject(request.json['username'].lower(),
                                                request.json['password'],
                                                'admin')
        url = app.config["USERS_URL"]
        user_service = UserProxyAccess(url)
        created_user = user_service.create_a_new_user(user_to_post)
        return jsonify(created_user), 200
    else:
        #none admin
        raise InvalidUsage('Forbidden for this user', status_code=403)


@user_blueprint.route('/', methods=['GET'])
@jwt_required
def show_all_users():
    """
    Show all users from the backend.
    ---
    description: Protected content method. Can not be seen unless you are an admin.
    tags:
      - User Methods
    security:
      - APIKeyHeader: []
    responses:
      200:
        description: User successfully accessed the content.
    """
    current_user_role = get_jwt_identity()['role']
    url = app.config["USERS_URL"]
    user_service = UserProxyAccess(url)
    response = user_service.get_all_users()
    return jsonify(response), 200


@user_blueprint.route('/<username>', methods=['GET'])
def get_user_by_name(username):
    """
    Get a userid in the backend
    ---
    description: Query a user based on username
    tags:
      - User Methods
    parameters:
      - name: username
        type: string
        in: path
        required: true
        example: testadmin
        description: user you want to query
    responses:
      200:
        description: User successfully accessed the content.
    """
    url = app.config["USERS_URL"]
    user_service = UserProxyAccess(url)
    response = user_service.get_user_id(username)
    return jsonify(response.__dict__), 200


@user_blueprint.route('/<id>', methods=['DELETE'])
@jwt_required
def delete_a_user(id):
    """
    Delete a user in the backend
    ---
    description: Protected content method. Can not be seen without valid token. Only admins can delete users
    tags:
      - User Methods
    security:
      - APIKeyHeader: []
    parameters:
      - name: id
        type: string
        in: path
        required: true
        description: user to delete
        example: 4
    responses:
      200:
        description: User successfully accessed the content.
    """
    current_user_role = get_jwt_identity()['role']
    if current_user_role != 'admin':
        raise InvalidUsage('Forbidden for this user', status_code=403)
    else:
        # admin
        url = app.config["USERS_URL"]
        user_service = UserProxyAccess(url)
        user_service.delete_a_user(id)
        return '', 204


@user_blueprint.route('/<id>', methods=['PUT'])
@jwt_required
def update_a_user(id):
    """
    Update a user in the backend
    ---
    description: Protected content method. Can not be seen without valid token. Only admins can update users
    tags:
      - User Methods
    security:
      - APIKeyHeader: []
    parameters:
      - name: id
        type: string
        in: path
        required: true
        description: user to update
        example: 4
      - name: body
        in: body
        required: true
        schema:
          type: object
          properties:
            active:
              type: boolean
              example: true
    responses:
      204:
        description: User was updated
    """
    current_user_role = get_jwt_identity()['role']
    if current_user_role != 'admin':
        raise InvalidUsage('Forbidden for this user', status_code=403)
    else:
        # admin
        active = request.json['active']
        url = app.config["USERS_URL"]
        user_service = UserProxyAccess(url)
        user_service.update_a_user(id, active)
        return '', 204


@user_blueprint.errorhandler(InvalidUsage)
def handle_invalid_usage(error):
    response = jsonify(error.to_dict())
    response.status_code = error.status_code
    return response


