using MyRoguelike.Display;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VH.Engine.Display;

namespace SPARC.Display {
    public class SparcMessageManager: MessageManager {

        public SparcMessageManager(MessageWindow window): base(window) { }

        public override void ShowMessage(string key, AbstractEntity performer, AbstractEntity target, bool force) {
            string levelAttr;
            if (performer.Person == Person.Second) levelAttr = "person2level";
            else levelAttr = "person3level";
            string xpath = "/messages/message[@key='" + key + "']/@" + levelAttr;
            XmlNodeList nodes = doc.SelectNodes(xpath);
            string level = "";
            if (nodes.Count > 0) level = ((XmlAttribute)nodes[0]).Value;
            //
            SparcMessageWindow window = (SparcMessageWindow)this.window;
            if (level == "info") window.SetColor(ConsoleColor.White);
            if (level == "warn") window.SetColor(ConsoleColor.Yellow);
            if (level == "crit") window.SetColor(ConsoleColor.Red);
            //
            base.ShowMessage(key, performer, target, force);
        }

        public override void ShowDirectMessage(string message) {
            ((SparcMessageWindow)window).SetColor(ConsoleColor.Gray);
            base.ShowDirectMessage(message);
        }

    }
}
