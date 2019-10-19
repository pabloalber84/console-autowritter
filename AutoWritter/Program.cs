/*
 * AutoWritter
 * Fecha: 18/10/2019
 * Programado totalmente por: Pablo Alberto Espinoza Ruiz
 * Uso únicamente para fines educativos.
 */
using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace AutoWritter
{
    class Configs
    {
        public double vel_perline;
        public double vel_perletter;
        private string config_file = "config.xml";

        public Configs(string config_name = "config.xml")
        {
            config_file = config_name;
            LoadConfig();
        }
        public void Load(string config_name = "null")
        {
            if (config_name != "null" && config_file != config_name)
                config_file = config_name;
            LoadConfig();
        }
        private void LoadConfig()
        {
            string buff = File.ReadAllText(System.Environment.CurrentDirectory + "/" + config_file);
            ParseConfig(buff);
        }
        private void ParseConfig(string config_xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(config_xml);
            var confs = doc
                        .SelectNodes("/configs/config")
                        .Cast<XmlElement>()
                        .Select(x => new {
                            name = x.Attributes["name"].Value,
                            value = x.Attributes["value"].Value
                        });
            foreach (var item in confs)
            {
                switch (item.name)
                {
                    case "velocity_perline":
                        vel_perline = double.Parse(item.value);
                        break;
                    case "velocity_perletter":
                        vel_perletter = double.Parse(item.value);
                        break;
                    case "text_color":
                        if (item.value == "green")
                            Console.ForegroundColor = ConsoleColor.Green;
                        else if (item.value == "black")
                            Console.ForegroundColor = ConsoleColor.Black;
                        else if (item.value == "white")
                            Console.ForegroundColor = ConsoleColor.White;
                        else if (item.value == "red")
                            Console.ForegroundColor = ConsoleColor.Red;
                        else if (item.value == "blue")
                            Console.ForegroundColor = ConsoleColor.Blue;
                        else if (item.value == "yellow")
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case "background_color":
                        if (item.value == "green")
                            Console.BackgroundColor = ConsoleColor.Green;
                        else if (item.value == "black")
                            Console.BackgroundColor = ConsoleColor.Black;
                        else if (item.value == "white")
                            Console.BackgroundColor = ConsoleColor.White;
                        else if (item.value == "red")
                            Console.BackgroundColor = ConsoleColor.Red;
                        else if (item.value == "blue")
                            Console.BackgroundColor = ConsoleColor.Blue;
                        else if (item.value == "yellow")
                            Console.BackgroundColor = ConsoleColor.Yellow;
                        break;
                }
            }
        }
    }
    class AutoWritter
    {
        private string _file_name;
        private List<string> _file_data;
        private Configs _configs;

        public AutoWritter(string file_name = "data.txt", string config_file = "config.xml")
        {
            _configs = new Configs(config_file);
            _file_name = file_name;
            LoadFile();
        }
        public void ReloadConfigsFromFile(string config_file)
        {
            _configs.Load(config_file);
        }
        public void ReloadTextFromFile(string file_name)
        {
            _file_name = file_name;
            LoadFile();
        }
        private void LoadFile()
        {
            _file_data = new List<string>(File.ReadLines(System.Environment.CurrentDirectory + "/" + _file_name));
        }
        public void Run()
        {
            Console.WriteLine();
            foreach (string buff in _file_data)
            {
                for (int i = 0; i < buff.Length; i++)
                {
                    Console.Write(buff[i]);
                    Thread.Sleep((int)(1000 * _configs.vel_perletter));
                }
                Thread.Sleep((int)(1000 * _configs.vel_perline));
                Console.WriteLine();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Auto Writter by PAER =)";
            AutoWritter aw = new AutoWritter("data.txt", "config.xml");
            aw.Run();
            Console.ReadKey();
        }
    }
}
