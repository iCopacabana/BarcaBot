from flask import Flask, make_response
from flask_restful import Resource, Api, request
from image_manipulator import AttackerCardCreator, MidfielderCardCreator, DefenderCardCreator, GoalieCardCreator

app = Flask(__name__)
api = Api(app)

class Attacker(Resource):
    def post(self):
        player = request.get_json()

        d = AttackerCardCreator('attacker_blank', player)
        d.prepare_for_drawing()
        c = d.get_card()
        
        return make_response(c)

class Midfielder(Resource):
    def post(self):
        player = request.get_json()

        d = MidfielderCardCreator('midfielder_blank', player)
        d.prepare_for_drawing()
        c = d.get_card()
        
        return make_response(c)

class Defender(Resource):
    def post(self):
        player = request.get_json()

        d = DefenderCardCreator('defender_blank', player)
        d.prepare_for_drawing()
        c = d.get_card()
        
        return make_response(c)

class Goalie(Resource):
    def post(self):
        player = request.get_json()

        d = GoalieCardCreator('goalie_blank', player)
        d.prepare_for_drawing()
        c = d.get_card()
        
        return make_response(c)

api.add_resource(Attacker, '/player_cards/attacker/')
api.add_resource(Midfielder, '/player_cards/midfielder/')
api.add_resource(Defender, '/player_cards/defender/')
api.add_resource(Goalie, '/player_cards/goalie/')