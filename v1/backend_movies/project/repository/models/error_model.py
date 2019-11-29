class ErrorModel:
    # Common base class for all mongo objects

    def __init__(self, title, message):
        self.title = title
        self.message = message