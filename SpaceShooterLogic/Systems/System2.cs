using System.Collections.Generic;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Systems
{
    public abstract class System2
    {
        public void Process(GameState state, List<IGameComponent> list1)
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
                    ProcessSingleEntity(state, component1);
                    cursor1++;
                }
            } while (stayInLoop);
        }

        public void Process(GameState state, List<IGameComponent> list1, List<IGameComponent> list2)
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

        public void Process(GameState state, List<IGameComponent> list1, List<IGameComponent> list2, List<IGameComponent> list3)
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

        //public static void Process(GameState state, params List<IGameComponent>[] lists)
        //{
        //    var cursor1 = 0;
        //    var cursor2 = 0;
        //    var cursor3 = 0;

        //    bool stayInLoop = true;
        //    do
        //    {
        //        if (cursor1 == lists[0].Count || cursor2 == lists[1].Count || cursor3 == lists[2].Count)
        //        {
        //            stayInLoop = false;
        //        }
        //        else
        //        {
        //            var component1 = lists[0][cursor1];
        //            var component2 = lists[1][cursor2];
        //            var component3 = lists[2][cursor3];

        //            if (component1.EntityId == component2.EntityId)
        //            {
        //                if (component2.EntityId == component3.EntityId)
        //                {
        //                    // we have a match!
        //                    ProcessOne(component1, component2, component3, state);
        //                    cursor1++;
        //                    cursor2++;
        //                    cursor3++;
        //                }
        //                else
        //                {
        //                    if (component2.EntityId < component3.EntityId)
        //                    {
        //                        cursor2++;
        //                    }
        //                    else
        //                    {
        //                        cursor3++;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (component1.EntityId < component2.EntityId)
        //                {
        //                    cursor1++;
        //                }
        //                else
        //                {
        //                    cursor2++;
        //                }
        //            }
        //        }
        //    } while (stayInLoop);
        //}

        protected abstract void ProcessSingleEntity(GameState state, params IGameComponent[] components);
    }
}