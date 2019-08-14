from gevent.pywsgi import WSGIServer
from server import app

print('Serving on http://localhost:4000')
http_server = WSGIServer(('', 4000), app)
http_server.serve_forever()