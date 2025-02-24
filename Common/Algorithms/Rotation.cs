﻿using System;
using Common.Grid;

namespace Common.Algorithms
{
    public static class Rotation
    {
        /// <summary>
        /// Finds out the direction that the rotation will be
        /// </summary>
        /// <param name="charactersXPos">The array of X positions for the characters</param>
        /// <param name="charactersWeight">The array of weights for the characters</param>
        /// <returns>The multiplier for direction of rotation (1 or -1)</returns>
        public static int RotationMultiplier(double[] charactersXPos, double[] charactersWeight)
        {
            double totalMomentFromCentre = 0;
            for (int i = 0; i < charactersXPos.Length; i++)
            {
                //The distance from the centre line of the board relative to the rotation
                double relativeDistanceFromPivot = 200 - charactersXPos[i];

                //Figure out the turning moment for this item
                double moment = relativeDistanceFromPivot * charactersWeight[i];

                //Sum the turning moments
                totalMomentFromCentre += moment;
            }
            if (totalMomentFromCentre == 0)
            {
                //If there's no moment, don't try to divide by 0
                return 0;
            }
            else
            {
                //Prevents divide by 0 error
                return (int)-(totalMomentFromCentre / Math.Abs(totalMomentFromCentre));
            }
        }

        /// <summary>
        /// Gets the difference in angle between the time now and from a point of time in the future.
        /// </summary>
        /// <param name="charactersPos">The array of character positions</param>
        /// <param name="time">The time between now and when you want to work out the angle for</param>
        /// <param name="weights">The array of weights of the characters</param>
        /// <returns>The difference in angle</returns>
        public static double AbsAngleDelta(Position[] charactersPos, double time, double[] weights)
        {
            double velocity = 9.8; //assume velocity = acceleration * time = 1, assume acceleration is due to gravity
            double totalDeltaRadians = 0;

            for (int i = 0; i < charactersPos.Length; i++)
            {
                //Work out the x and y components of the radius from centre.
                //Weights are included to simulate effect of heavier characters causing more rotation
                double radiusFromCentrex = 200 - charactersPos[i].x * weights[i];
                double radiusFromCentrey = 200 - charactersPos[i].y * weights[i];
                //c=sqrt(a^2+b^2) (pythagoras to find t)
                double radiusFromCentre = Math.Sqrt(Math.Pow(radiusFromCentrex, 2) + Math.Pow(radiusFromCentrey, 2));
                //Sum
                totalDeltaRadians += (velocity * time) / radiusFromCentre;
            }

            return Math.Abs(totalDeltaRadians / Math.PI)*180;
        }
    }
}