namespace AdventOfCode2023.CSharp.Day02;

public class Game {

    public int Id {get; set;}

    public int Blue {get; set;}

    public int Green {get; set;}

    public int Red {get; set;}

    // Ex: Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
    public Game(string line) {
        Blue = 0;
        Green = 0;
        Red = 0;

        var gameId = line.Split(':', StringSplitOptions.TrimEntries);
        Id = int.Parse(gameId[0].Split(' ')[1]);

        var pulls = gameId[1].Split(';', StringSplitOptions.TrimEntries);
        foreach(var pull in pulls) {
            foreach(var colorGroup in pull.Split(',', StringSplitOptions.TrimEntries)) {
                var num = int.Parse(colorGroup.Split(' ')[0]);
                var color = colorGroup.Split(' ')[1];

                switch (color) {
                    case "blue":
                        Blue = num > Blue ? num : Blue;
                        break;
                    case "green":
                        Green = num > Green ? num : Green;
                        break;
                    case "red":
                        Red = num > Red ? num : Red;
                        break;
                    default:
                        throw new Exception($"{color} not an allowed color");
                }
            }
        }
    }

    public bool IsGamePossible(int maxBlue, int maxGreen, int maxRed) {
        return maxBlue >= Blue && maxGreen >= Green && maxRed >= Red;
    }

    public int PowerOfLeastCubes() {
        return Blue * Green * Red;
    }
}