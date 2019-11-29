class MovieObject:
    # Common base class for all mongo objects

    def __init__(self, title, year, imdb, type, description, image):
        self.title = title
        self.year = year
        self.imdb = imdb
        self.type = type
        self.image = image
        self.description = description

class SuccessModel():
    def __init__(self, message, body):
        self.message = message
        self.body = body