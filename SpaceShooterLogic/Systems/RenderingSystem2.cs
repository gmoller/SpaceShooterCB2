using System.Collections.Generic;
using GameEngineCore;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;
using IGameComponent = SpaceShooterLogic.Components.IGameComponent;

namespace SpaceShooterLogic.Systems
{
    public class RenderingSystem2
    {
        // needs Transform, Texture and Volume

        private static void ProcessSingleEntity(GameState state, IGameComponent component1, IGameComponent component2, IGameComponent component3)
        //private static void ProcessSingleEntity(GameState state, params IGameComponent[] components)
        {
            var t = (Transform2)component1;
            var tex = (Texture2)component2;
            var v = (Volume2)component3;
            state.AddToSpriteBatchList(tex.Texture, t.Transform.Position, new RectangleF(0, 0, 16, 16), t.Transform.Rotation, new Vector2(8.0f), t.Transform.Scale, new Rectangle((int)v.X, (int)v.Y, (int)v.Width, (int)v.Height));
        }

        public static void Process(GameState state, List<IGameComponent> list1)
        {
            var cursor1 = 0;

            bool stayInLoop = true;
            do
            {
                if (cursor1 == list1.Count)
                {
                    stayInLoop = false;
                }
                else
                {
                    var component1 = list1[cursor1];

                    // we have a match!
                    ProcessSingleEntity(state, component1, null, null);
                    cursor1++;
                }
            } while (stayInLoop);
        }

        public static void Process(GameState state, List<IGameComponent> list1, List<IGameComponent> list2)
        {
            var cursor1 = 0;
            var cursor2 = 0;

            bool stayInLoop = true;
            do
            {
                if (cursor1 == list1.Count || cursor2 == list2.Count)
                {
                    stayInLoop = false;
                }
                else
                {
                    var component1 = list1[cursor1];
                    var component2 = list2[cursor2];

                    if (component1.EntityId == component2.EntityId)
                    {
                        // we have a match!
                        ProcessSingleEntity(state, component1, component2, null);
                        cursor1++;
                        cursor2++;
                    }
                    else
                    {
                        if (component1.EntityId < component2.EntityId)
                        {
                            cursor1++;
                        }
                        else
                        {
                            cursor2++;
                        }
                    }
                }
            } while (stayInLoop);
        }

        public static void Process(GameState state, List<IGameComponent> list1, List<IGameComponent> list2, List<IGameComponent> list3)
        {
            var cursor1 = 0;
            var cursor2 = 0;
            var cursor3 = 0;

            bool stayInLoop = true;
            do
            {
                if (cursor1 == list1.Count || cursor2 == list2.Count || cursor3 == list3.Count)
                {
                    stayInLoop = false;
                }
                else
                {
                    var component1 = list1[cursor1];
                    var component2 = list2[cursor2];
                    var component3 = list3[cursor3];

                    if (component1.EntityId == component2.EntityId)
                    {
                        if (component2.EntityId == component3.EntityId)
                        {
                            // we have a match!
                            ProcessSingleEntity(state, component1, component2, component3);
                            cursor1++;
                            cursor2++;
                            cursor3++;
                        }
                        else
                        {
                            if (component2.EntityId < component3.EntityId)
                            {
                                cursor2++;
                            }
                            else
                            {
                                cursor3++;
                            }
                        }
                    }
                    else
                    {
                        if (component1.EntityId < component2.EntityId)
                        {
                            cursor1++;
                        }
                        else
                        {
                            cursor2++;
                        }
                    }
                }
            } while (stayInLoop);
        }
    }
}