using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Xml;

namespace TheReturnOfTheKing
{
    public class MenuManager : GameObjectManager
    {
        public override VisibleGameObject CreateObject(int idx)
        {
            return base.CreateObject(idx);
        }

        public override bool InitPrototypes(ContentManager content, string fileName)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlNodeList list = doc.SelectNodes(@"//Menu");
                _nprototype = list.Count;
                _prototype = new Menu[_nprototype];

                for (int i = 0; i < _nprototype; ++i)
                {
                    /*_prototype[i] = new Menu();
                    _prototype[i]._nsprite = 0;
                    _prototype[i]._sprite = null;
                    _prototype[i].X = 0;
                    _prototype[i].Y = 0;*/

                    _prototype[i] = new Menu();
                    _prototype[i]._nsprite = 1;
                    _prototype[i]._sprite = new GameSprite[_prototype[i]._nsprite];

                    _prototype[i]._sprite[0] = new GameSprite(content.Load<Texture2D>(list[i].SelectSingleNode(@"Background").InnerText),
                        0,
                        0);
                    _prototype[i].Height = int.Parse(list[i].SelectSingleNode(@"Height").InnerText);
                    _prototype[i].Width = int.Parse(list[i].SelectSingleNode(@"Width").InnerText);
                    _prototype[i].X = int.Parse(list[i].SelectSingleNode(@"X").InnerText);
                    _prototype[i].Y = int.Parse(list[i].SelectSingleNode(@"Y").InnerText);
                    _prototype[i].Rect = new Rectangle((int)_prototype[i].X, (int)_prototype[i].Y, (int)_prototype[i].Width, (int)_prototype[i].Height);
                    ((Menu)_prototype[i]).DelayTime = int.Parse(list[i].SelectSingleNode(@"DelayTime").InnerText);

                    MotionInfo _menuMoveInfo = new MotionInfo();
                    XmlNode moveInfo = list[i].SelectSingleNode(@"MoveInfo");

                    _menuMoveInfo.FirstDection = moveInfo.SelectSingleNode(@"FirstDirection").InnerText;
                    if (_menuMoveInfo.FirstDection == "Null")
                        _menuMoveInfo.IsStanding = true;
                    else
                        _menuMoveInfo.IsStanding = false;

                    string temp = moveInfo.SelectSingleNode(@"StandingGround").InnerText;
                    if (temp == "Null")
                        _menuMoveInfo.StandingGround = float.MinValue;
                    else
                        _menuMoveInfo.StandingGround = float.Parse(temp);

                    _menuMoveInfo.Vel = new Vector2(float.Parse(moveInfo.SelectSingleNode(@"Velocity").SelectSingleNode(@"X").InnerText),
                        int.Parse(moveInfo.SelectSingleNode(@"Velocity").SelectSingleNode(@"Y").InnerText));

                    _menuMoveInfo.Accel = new Vector2(float.Parse(moveInfo.SelectSingleNode(@"Acceleration").SelectSingleNode(@"X").InnerText),
                        int.Parse(moveInfo.SelectSingleNode(@"Acceleration").SelectSingleNode(@"Y").InnerText));

                    _menuMoveInfo.DecelerationRate = float.Parse(moveInfo.SelectSingleNode(@"DecelerationRate").InnerText);
                    _menuMoveInfo.Owner = _prototype[i];
                    ((Menu)_prototype[i])._motionInfo = _menuMoveInfo;

                    for (int j = 0; j < list[i].ChildNodes.Count; ++j)
                    {
                        if (list[i].ChildNodes[j].Name.CompareTo("Button") == 0)
                        {
                            XmlNode _node = list[i].ChildNodes[j];
                            Button _b = (Button)GlobalVariables.Btm.CreateObject(int.Parse(_node.SelectSingleNode("Type").InnerText));
                            _b.X = int.Parse(_node.SelectSingleNode("X").InnerText);
                            _b.Y = int.Parse(_node.SelectSingleNode("Y").InnerText);
                            _b.DelayTime = int.Parse(_node.SelectSingleNode("DelayTime").InnerText);
                            _b.Rect = new Rectangle((int)_b.X, (int)_b.Y, (int)_b.Width, (int)_b.Height);

                            MotionInfo _buttonMoveInfo = new MotionInfo();
                            moveInfo = _node.SelectSingleNode(@"MoveInfo");

                            _buttonMoveInfo.Owner = _b;
                            _buttonMoveInfo.FirstDection = moveInfo.SelectSingleNode(@"FirstDirection").InnerText;
                            if (_buttonMoveInfo.FirstDection == "Null")
                                _buttonMoveInfo.IsStanding = true;
                            else
                                _buttonMoveInfo.IsStanding = false;

                            temp = moveInfo.SelectSingleNode(@"StandingGround").InnerText;
                            if (temp == "Null")
                                _buttonMoveInfo.StandingGround = float.MinValue;
                            else
                                _buttonMoveInfo.StandingGround = float.Parse(temp);

                            _buttonMoveInfo.Vel = new Vector2(float.Parse(moveInfo.SelectSingleNode(@"Velocity").SelectSingleNode(@"X").InnerText),
                                int.Parse(moveInfo.SelectSingleNode(@"Velocity").SelectSingleNode(@"Y").InnerText));

                            _buttonMoveInfo.Accel = new Vector2(float.Parse(moveInfo.SelectSingleNode(@"Acceleration").SelectSingleNode(@"X").InnerText),
                                int.Parse(moveInfo.SelectSingleNode(@"Acceleration").SelectSingleNode(@"Y").InnerText));

                            _buttonMoveInfo.DecelerationRate = float.Parse(moveInfo.SelectSingleNode(@"DecelerationRate").InnerText);
                            _b._motionInfo = _buttonMoveInfo;
                            ((Menu)_prototype[i]).Child.Add(_b);
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        } 
    }
}
