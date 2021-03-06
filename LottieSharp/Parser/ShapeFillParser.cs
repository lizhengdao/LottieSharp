﻿using LottieSharp.Model.Animatable;
using LottieSharp.Model.Content;

namespace LottieSharp.Parser
{
    static class ShapeFillParser
    {
        internal static ShapeFill Parse(JsonReader reader, LottieComposition composition)
        {
            AnimatableColorValue color = null;
            bool fillEnabled = false;
            AnimatableIntegerValue opacity = null;
            string name = null;
            int fillTypeInt = 1;

            while (reader.HasNext())
            {
                switch (reader.NextName())
                {
                    case "nm":
                        name = reader.NextString();
                        break;
                    case "c":
                        color = AnimatableValueParser.ParseColor(reader, composition);
                        break;
                    case "o":
                        opacity = AnimatableValueParser.ParseInteger(reader, composition);
                        break;
                    case "fillEnabled":
                        fillEnabled = reader.NextBoolean();
                        break;
                    case "r":
                        fillTypeInt = reader.NextInt();
                        break;
                    default:
                        reader.SkipValue();
                        break;
                }
            }

            var fillType = fillTypeInt == 1 ? PathFillType.Winding : PathFillType.EvenOdd;
            return new ShapeFill(name, fillEnabled, fillType, color, opacity);
        }
    }
}
