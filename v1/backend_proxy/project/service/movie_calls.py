import requests
import json
from json import JSONDecodeError
from project.middleware.error_handler import InvalidUsage


class MovieProxyAccess(object):

    def __init__(self, url):
        self.url = url

    def get_movie_id_based(self, id):
        r = requests.get(self.url + '/' + id)
        return json.loads(r.text)

    def get_movie_with_search_param(self, search_term):
        r = requests.get(self.url + '?search=' + search_term)
        if r.status_code != 200:
            self.handle_non_200_status(r.status_code)
        return json.loads(r.text)

    def get_all_movies(self):
        r = requests.get(self.url)
        return json.loads(r.text)

    def create_a_new_movie(self, body):
        r = requests.post(self.url, body)
        if r.status_code != 200:
            self.handle_non_200_status(r.status_code)
        return json.loads(r.text)

    @staticmethod
    def handle_non_200_status(response_code):
        codes = { 400: "Server returned a bad request call failed",
                  404: "Server returned a not found",
                  501: "Server returned a not implemented - service unavailable",
                  500: "Server unavailable"
                   }

        f = codes[response_code]
        raise InvalidUsage(message=f, status_code=response_code)


