import io
from name_converter import convert_name
from PIL import Image, ImageDraw, ImageFont

name_font = ImageFont.truetype('fonts/PTSans-Bold.ttf', 62)
regular_font = ImageFont.truetype('fonts/PTSans-Regular.ttf', 16)
regular_big_font = ImageFont.truetype('fonts/PTSans-Regular.ttf', 27)

def image_to_bytes(image:Image):
    bytes = io.BytesIO()
    image.save(bytes, 'PNG')
    
    return bytes.getvalue()

class StringDrawer:
    def __init__(self, drawing_context):
        self.d = drawing_context
    
    # method to draw the player name at the top of the card
    def draw_name(self, position, name):
        #22 ,17
        self.d.text(position, name, font=name_font, fill=('#ebebeb'))
    
    # method to draw all the regular values
    def draw_regular(self, position, string):
        self.d.text(position, string, font=regular_font, fill=('#ebebeb'))

    # method to draw all the regular big values (the ones at the bottom of the card)
    def draw_regular_big(self, position, string):
        self.d.text(position, string, font=regular_big_font, fill=('#ebebeb'))

class CardCreator:
    def __init__(self, preset_image_name, player_dict:dict):
        self.image_name = preset_image_name
        self.player_dict = player_dict

    def prepare_for_drawing(self):
        self.base_image = Image.open(f'images/{self.image_name}.png')
        self.txt = Image.new('RGBA', self.base_image.size, (255,255,255,0))
        self.drawing_context = ImageDraw.Draw(self.txt)
        self.string_drawer = StringDrawer(self.drawing_context)
    
    def draw_basic_info(self):
        name = convert_name(self.player_dict['Name']).upper()

        # name
        self.string_drawer.draw_name((22,17), name)
        # position
        self.string_drawer.draw_regular((25,120), self.player_dict['Position'])
        # age
        self.string_drawer.draw_regular((114,120), str(self.player_dict['Age']))
        # nationality
        self.string_drawer.draw_regular((169,120), self.player_dict['Nationality'])
        # height
        self.string_drawer.draw_regular((285,120), self.player_dict['Height'])
        # weight
        self.string_drawer.draw_regular((367,120), self.player_dict['Weight'])    

class AttackerCardCreator(CardCreator):
    def draw_attacker_regular(self):
        # shots
        # total
        self.string_drawer.draw_regular((25,186), str(self.player_dict['Per90Stats']['Shots']['Total']))
        # on target
        self.string_drawer.draw_regular((142,186), f"{str(self.player_dict['Per90Stats']['Shots']['OnTarget'])} ({str(self.player_dict['Per90Stats']['Shots']['PercentageOnTarget'])}%)")

        # dribbles
        # attempted
        self.string_drawer.draw_regular((25,254), str(self.player_dict['Per90Stats']['Dribbles']['Attempted']))
        # successful
        self.string_drawer.draw_regular((197,254), f"{str(self.player_dict['Per90Stats']['Dribbles']['Won'])} ({str(self.player_dict['Per90Stats']['Dribbles']['PercentageWon'])}%)")
    
    def draw_attacker_bottom_big(self):
        # bottom big stats
        # goals
        self.string_drawer.draw_regular_big((25,330), str(self.player_dict['Goals']['Total']))
        # assists
        self.string_drawer.draw_regular_big((155,330), str(self.player_dict['Goals']['Assists']))
        # rating
        self.string_drawer.draw_regular_big((304,330), str(self.player_dict['Rating']))
    
    def get_card(self):
        self.draw_basic_info()
        self.draw_attacker_regular()
        self.draw_attacker_bottom_big()

        out = Image.alpha_composite(self.base_image, self.txt)
        
        return image_to_bytes(out)

class MidfielderCardCreator(CardCreator):
    def draw_midfielder_regular(self):
        # key passes
        self.string_drawer.draw_regular((25,186), str(self.player_dict['Per90Stats']['Passes']['KeyPasses']))
        # pass accuracy
        self.string_drawer.draw_regular((142,186), f"{str(self.player_dict['Per90Stats']['Passes']['Accuracy'])}%")

        # tackles
        self.string_drawer.draw_regular((25,254), str(self.player_dict['Per90Stats']['Tackles']['TotalTackles']))
        # interceptions
        self.string_drawer.draw_regular((114,254), str(self.player_dict['Per90Stats']['Tackles']['Interceptions']))
    
    def draw_midfielder_bottom_big(self):
        # bottom big stats
        # goals
        self.string_drawer.draw_regular_big((25,330), str(self.player_dict['Goals']['Total']))
        # assists
        self.string_drawer.draw_regular_big((155,330), str(self.player_dict['Goals']['Assists']))
        # rating
        self.string_drawer.draw_regular_big((304,330), str(self.player_dict['Rating']))
    
    def get_card(self):
        self.draw_basic_info()
        self.draw_midfielder_regular()
        self.draw_midfielder_bottom_big()

        out = Image.alpha_composite(self.base_image, self.txt)
        
        return image_to_bytes(out)

class DefenderCardCreator(CardCreator):
    def draw_defender_regular(self):
        # tackles
        self.string_drawer.draw_regular((25,186), str(self.player_dict['Per90Stats']['Tackles']['TotalTackles']))
        # blocks
        self.string_drawer.draw_regular((142,186), str(self.player_dict['Per90Stats']['Tackles']['Blocks']))

        # interceptions
        self.string_drawer.draw_regular((25,254), str(self.player_dict['Per90Stats']['Tackles']['Interceptions']))
        # duels won
        self.string_drawer.draw_regular((197,254), f"{str(self.player_dict['Per90Stats']['Duels']['Won'])} ({str(self.player_dict['Per90Stats']['Duels']['PercentageWon'])}%)")
    
    def draw_defender_bottom_big(self):
        # bottom big stats
        # rating
        self.string_drawer.draw_regular_big((25,330), str(self.player_dict['Rating']))
    
    def get_card(self):
        self.draw_basic_info()
        self.draw_defender_regular()
        self.draw_defender_bottom_big()

        out = Image.alpha_composite(self.base_image, self.txt)
        
        return image_to_bytes(out)

class GoalieCardCreator(CardCreator):    
    def draw_goalie_bottom_big(self):
        # bottom big stats
        # goals conceded
        self.string_drawer.draw_regular_big((25,330), str(self.player_dict['Goals']['Conceded']))
        # rating
        self.string_drawer.draw_regular_big((290,330), str(self.player_dict['Rating']))
    
    def get_card(self):
        self.draw_basic_info()
        self.draw_goalie_bottom_big()

        out = Image.alpha_composite(self.base_image, self.txt)
        
        return image_to_bytes(out)