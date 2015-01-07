using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

public static class DefaultValues
{
    //parsing
    public static int City_cols = 4;
    public static int Connections_cols = 3;
    public static int PP_cols = 4;

    //DRAWING

    //Cities
    public static int City_gui_size = 15;
    public static float City_gui_pen_size = 2;
    public static float City_gui_height_name_rect = 11;
    public static float Font_size = 7;
    //Connections
    public static float Connection_gui_pen_size = 2;
    public static int Connection_gui_r = 8;
    //PowerPlants
    public static int PP_frame_size = 80;
    public static int PP_inside_frame_size = PP_frame_size - 14;
    public static int PP_price_font_size = 22;
    public static int PP_cities_font_size = 14;
    //Player
    public static int Player_size = 48;


    //GameModel

    public static int Max_nb_of_players = 6;
    public static int Nb_of_districts = 3;

    public static Color[] resource_colors =
    {
        Color.Brown,
        Color.Black,
        Color.Yellow,
        Color.Red
    };
    public static int Resource_size = 14;
    public static int Resource_pen_size = 1;
    public static Color Resource_pen_color = Color.Black;
}

