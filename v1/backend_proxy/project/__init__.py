import os
from flask import Flask, jsonify
from flasgger import Swagger


def create_app():
    # instantiate the app
    swagger_config = {
        "headers": [
        ],
        "specs": [
            {
                "endpoint": 'swagger',
                "route": '/swagger.json',
                "rule_filter": lambda rule: True,  # all in
                "model_filter": lambda tag: True,  # all in
            }

        ],
        "static_url_path": "/flasgger_static",
        # "static_folder": "static",  # must be set by user
        "specs_route": "/swagger/"
    }

    app = Flask(__name__)
    app.config['SWAGGER'] = {
            "swagger": "2.0",
            'uiversion': "3",
            "info": {
                "title": "Flask Proxy Api",
                "version": "1.0"

            },
    "securityDefinitions": {
        "APIKeyHeader": {
            "type": "apiKey",
            "name": "Authorization",
            "in": "header"
        },
    }
    }

    Swagger(app, config=swagger_config)

    # set config
    app_settings = 'project.config.' + os.getenv('APP_SETTINGS')
    app.config.from_object(app_settings)

    # register blueprints
    from project.api.token_controller import proxy_blueprint
    app.register_blueprint(proxy_blueprint, url_prefix='/v1/proxy/tokens')

    from project.api.user_controller import user_blueprint
    app.register_blueprint(user_blueprint, url_prefix='/v1/proxy/users')

    from project.api.movie_controller import movie_blueprint
    app.register_blueprint(movie_blueprint, url_prefix='/v1/proxy/movies')

    return app
