from flask import jsonify, request, Blueprint
from flask import current_app as app
from flask_jwt_extended import jwt_required, get_jwt_identity
from project.middleware.error_handler import InvalidUsage
from project.service.movie_calls import MovieProxyAccess
from project.service.validator import DataValidator

movie_blueprint = Blueprint('movie', __name__,)


@movie_blueprint.route('/admin', methods=['GET'])
@jwt_required
def protected():
    """
    Protected content method.
    ---
    description: Protected content method. Can not be seen without valid token.
    tags:
      - Movie Methods
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


@movie_blueprint.route('/', methods=['GET'])
def get_all_movies():
    """
    Protected content method.
    ---
    description: Protected content method. Can not be seen without valid token.
    tags:
      - Movie Methods
    responses:
      200:
        description: User successfully accessed the content.
    """
    url = app.config["MOVIES_URL"]
    movie_service = MovieProxyAccess(url)
    response = movie_service.get_all_movies()
    return jsonify(response), 200


@movie_blueprint.route('/<id>', methods=['GET'])
@jwt_required
def get_specific_movie(id):
    """
    Protected content method.
    ---
    description: Protected content method. Can not be seen without valid token.
    tags:
      - Movie Methods
    security:
      - APIKeyHeader: []
    parameters:
      - name: id
        type: string
        in: path
        required: true
        description: specific movie you want to query
        example: tt0064116
    responses:
      200:
        description: User successfully accessed the content.
    """
    DataValidator.validate_id(id)
    url = app.config["MOVIES_URL"]
    movie_service = MovieProxyAccess(url)
    response = movie_service.get_movie_id_based(id)
    return jsonify(response), 200


@movie_blueprint.route('/searches', methods=['GET'])
@jwt_required
def get_movies_with_search_term():
    """
    Protected content method.
    ---
    description: Protected content method. Can not be seen without valid token.
    tags:
      - Movie Methods
    security:
      - APIKeyHeader: []
    parameters:
      - name: query
        type: string
        in: query
        required: true
        description: specific movie you want to query
        example: star
    responses:
      200:
        description: User successfully accessed the content.
    """
    query = request.args.get('query')
    DataValidator.validate_query(query)
    url = app.config["MOVIES_URL"]
    movie_service = MovieProxyAccess(url)
    response = movie_service.get_movie_with_search_param(query)
    return jsonify(response), 200



@movie_blueprint.route('/', methods=['POST'])
@jwt_required
def post_a_new_movie():
    """
        Endpoint to post a new movie
        ---
        tags:
          - Movie Methods
        security:
          - APIKeyHeader: []
        parameters:
          - name: body
            in: body
            required: true
            schema:
              properties:
                title:
                  type: string
                  description: The title to insert
                year:
                  type: string
                  description: The year the movie is from
                imdb:
                  type: string
                  description: Some imdb code
                type:
                  type: string
                  description: The type
              example:
                title: Testing goes wild
                year: 2018
                imdb: whatever
                type: movie
        responses:
          400:
            description: Incorrect data used
          200:
            description: Your movie query is correct
    """
    json_body = request.json
    current_user_role = get_jwt_identity()['role']
    if current_user_role == 'admin':
        # admin
        url = app.config["MOVIES_URL"]
        movie_service = MovieProxyAccess(url)
        movie_service.create_a_new_movie(json_body)
        return '', 201
    else:
        #none admin
        raise InvalidUsage('Forbidden for this user', status_code=403)

@movie_blueprint.errorhandler(InvalidUsage)
def handle_invalid_usage(error):
    response = jsonify(error.to_dict())
    response.status_code = error.status_code
    return response