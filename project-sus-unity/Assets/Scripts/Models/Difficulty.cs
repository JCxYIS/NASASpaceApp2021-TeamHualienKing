using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Difficulty
{
    public string Title;
    public float Challenge_BasePercent = 70;
    public float Challenge_BaseInterval = 45;
    public float MoveSpeed_Scale = 1;
    public int Game_Length = 183;

    public static List<Difficulty> HahaLol()
    {
        return new List<Difficulty>{
            // 0
            new Difficulty{
                Title = "Normal",
                Challenge_BasePercent = 70,
                Challenge_BaseInterval = 45,
                MoveSpeed_Scale = 1,
                Game_Length = 125,
            },

            // 1
            new Difficulty{
                Title = "Hard",
                Challenge_BasePercent = 75,
                Challenge_BaseInterval = 40,
                MoveSpeed_Scale = 0.87f,
                Game_Length = 183,
            },

            // 2
            new Difficulty{
                Title = "Super Hard",
                Challenge_BasePercent = 80,
                Challenge_BaseInterval = 35,
                MoveSpeed_Scale = 0.78f,
                Game_Length = 200,
            },

            // 3
            new Difficulty{
                Title = "Ultra Hard",
                Challenge_BasePercent = 83,
                Challenge_BaseInterval = 30,
                MoveSpeed_Scale = 0.69f,
                Game_Length = 222,
            },

            // 4
            new Difficulty{
                Title = "Master",
                Challenge_BasePercent = 86,
                Challenge_BaseInterval = 25,
                MoveSpeed_Scale = 0.6f,
                Game_Length = 222,
            },

            // 5
            new Difficulty{
                Title = "OMG",
                Challenge_BasePercent = 94.87f,
                Challenge_BaseInterval = 20,
                MoveSpeed_Scale = 0.5f,
                Game_Length = 222,
            },

            // 6
            new Difficulty{
                Title = "Cthulhu",
                Challenge_BasePercent = 96,
                Challenge_BaseInterval = 15,
                MoveSpeed_Scale = 4.8763f,
                Game_Length = 222,
            },

            // 7
            new Difficulty{
                Title = "<b>REAL</b> Challenge",
                // Challenge_BasePercent = 10,
                // Challenge_BaseInterval = 7,
                // MoveSpeed_Scale = 4.8763f,
                Game_Length = 7*30*24*60*60,
            },
        };
    }
}