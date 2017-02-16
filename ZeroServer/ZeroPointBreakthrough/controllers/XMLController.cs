
using System.Xml;
using System.Text;
using System.Xml.Linq;
using System;
using System.IO;
static class XMLController {


    static XmlWriterSettings ObjSetting(){
        XmlWriterSettings objSetting = new XmlWriterSettings();
        objSetting.Indent = true;
        objSetting.NewLineOnAttributes = true;
        return objSetting;
        
    }

    static void writeGameStateDocument(XmlWriter objWriter){
        objWriter.WriteStartDocument();
        writeMap(objWriter);

        objWriter.WriteEndDocument();


    }

    static void writePlayers(XmlWriter objWriter){
        objWriter.WriteStartElement("Players");

        writeMapTile(objWriter, 1,144,"pos");
        writeMapTile(objWriter, 3,11,"Somedddd");
        writeMapTile(objWriter, 2,22,"Someeeee");
        writeMapTile(objWriter, 53,33,"Somesssss");

        objWriter.WriteEndElement(); //books
    }

    static void writeMap(XmlWriter objWriter){
        objWriter.WriteStartElement("GameMap");

        writeMapTile(objWriter, 1,144,"Some");
        writeMapTile(objWriter, 3,11,"Somedddd");
        writeMapTile(objWriter, 2,22,"Someeeee");
        writeMapTile(objWriter, 53,33,"Somesssss");

        objWriter.WriteEndElement(); //books
    }
    static void writeMapTile(XmlWriter objWriter, int x, int y, string obj){
        objWriter.WriteStartElement("tile");

        writeAttr(objWriter, "X", x);
        writeAttr(objWriter, "Y", y);
        writeAttr(objWriter, "Object", obj);

        objWriter.WriteEndElement();
    }

    static void writePlayer(XmlWriter objWriter, int posX, int posY, string state){
        objWriter.WriteStartElement("Player");

        writePlayerPosition(objWriter,posX,posY);
        
        writeAttr(objWriter, "State", state);

        objWriter.WriteEndElement();
    }

    static void writePlayerPosition(XmlWriter objWriter, int posX, int posY){
        objWriter.WriteStartElement("Position");

        writeAttr(objWriter, "X", posX);
        writeAttr(objWriter, "Y", posY);

        objWriter.WriteEndElement();
    }

    static void writeAttr(XmlWriter objWriter, string a, int val){
        objWriter.WriteStartElement(a);
        objWriter.WriteValue(val);
        objWriter.WriteEndElement();
    }
    static void writeAttr(XmlWriter objWriter, string a, string val){
        objWriter.WriteStartElement(a);
        objWriter.WriteValue(val);
        objWriter.WriteEndElement();
    }

}