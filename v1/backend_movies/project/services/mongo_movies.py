from werkzeug.local import LocalProxy
from project.repository.mongo_connection import MongoConnection
from project.services.models.movie_model import SuccessModel
from project.repository.models.error_model import ErrorModel
from project.services.map_object_to_movie import MovieMapper
import pymongo
import re

from flask import g


def get_db():
    db = getattr(g, '_database', None)
    if db is None:
        db = g._database = MongoConnection.connect_to_mongo()
    return db


db = LocalProxy(get_db)


def get_movie_by_id(id):
    try:
        movie_col = db.movies
        specified_document = movie_col.find_one({'imdb': id})
        if specified_document is None:
            error_object = ErrorModel(id, "No movie found")
            return_object = {'status_code': 404, 'body': error_object}
            return return_object
        mapper = MovieMapper()
        movie_model = mapper.map_cursor_to_object(specified_document)
        return_object = {'status_code': 200, 'body': movie_model.__dict__}
        return return_object
    except pymongo.errors.OperationFailure:
        error_message = "oops, mongo error"
        error_object = ErrorModel(id, error_message)
        return_object = {'status_code': 500, 'body': error_object}
        return return_object


def get_list_of_movies():
    try:
        movie_col = db.movies
        specified_document = movie_col.find().limit(100)
        documents = []
        mapper = MovieMapper()
        for document in specified_document:
            movie_model = mapper.map_cursor_to_object(document)
            documents.append(movie_model.__dict__)
        return_object = {'status_code': 200, 'body': documents}
        return return_object
    except pymongo.errors.OperationFailure:
        error_message = "oops, mongo error"
        error_object = ErrorModel("All movies", error_message)
        return_object = {'status_code': 500, 'body': error_object}
        return return_object


def get_movie_by_search_term(search_term):
    try:
        movie_col = db.movies
        regx = re.compile(search_term + "*", re.IGNORECASE)
        specified_document = movie_col.find({'title': regx})
        if specified_document.count() == 0:
            error_object = ErrorModel("Nothing found for: " + search_term, "No movie found")
            return_object = {'status_code': 404, 'body': error_object}
            return return_object
        documents = []
        mapper = MovieMapper()
        for document in specified_document:
            movie_model = mapper.map_cursor_to_object(document)
            documents.append(movie_model.__dict__)
        return_object = {'status_code': 200, 'body': documents}
        return return_object
    except pymongo.errors.OperationFailure:
        error_message = "oops, mongo error"
        error_object = ErrorModel("Specific movie", error_message)
        return_object = {'status_code': 500, 'body': error_object}
        return return_object
    except TypeError:
        error_message = "oops, that cannot be done Bad programmer Error!"
        error_object = ErrorModel("Specific movie", error_message)
        return_object = {'status_code': 500, 'body': error_object}
        return return_object


def post_a_movie(mongo_object):
    try:
        movie_col = db.movies
        mapper = MovieMapper()
        post_model = mapper.map_cursor_to_object(mongo_object)
        insert_id = movie_col.insert_one(mongo_object).inserted_id
        if insert_id is not None:
            message = "Your list with id: {0} has been inserted".format(str(insert_id))
            post_model = SuccessModel(message, post_model.__dict__)
            return_object = {'status_code': 200, 'body': post_model.__dict__}
            return return_object
        else:
            error_message = 'Insert not successful, please make sure to post valid json'
            error_object = ErrorModel("All movies", error_message)
            return_object = {'status_code': 400, 'body': error_object}
            return return_object
    except pymongo.errors.OperationFailure:
        error_message = "oops, mongo error"
        error_object = ErrorModel("All movies", error_message)
        return_object = {'status_code': 500, 'body': error_object}
        return return_object


