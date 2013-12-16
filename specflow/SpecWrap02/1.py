# 
# * Created by SharpDevelop.
# * User: Alexander
# * Date: 12/17/2012
# * Time: 11:40 PM
# *
# * To change this template use Tools | Options | Coding | Edit Standard Headers.
# 
from System import *

class Program(object):
    def Main(args):
        Console.WriteLine("Hello World!")
        # TODO: Implement Functionality Here
        Console.Write("Press any key to continue . . . ")
        Console.ReadKey(True)

    Main = staticmethod(Main)