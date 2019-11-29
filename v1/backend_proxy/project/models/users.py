class UserObject:
    def __init__(self, username, role, user_id, active):
        self.username = username
        self.role = role
        self.id = user_id
        self.active = active


class CreationUserObject:
    def __init__(self, username, password, role, active=True):
        self.username = username
        self.password = password
        self.role = role
        self.active = active
