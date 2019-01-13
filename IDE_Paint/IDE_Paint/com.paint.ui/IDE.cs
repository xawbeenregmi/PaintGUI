﻿using IDE_Paint.com.paint.ui;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDE_Paint
{
    public partial class formIDE : MaterialForm
    {
        public int x, y;
        public string Syntaxcommand;
        
        public formIDE()
        {
            InitializeComponent();
           
        }

        /// <summary>
        /// action after run button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
           
            

           
           // Boolean syntaxChecked = SyntaxValidation(txtCommand);
            Boolean syntaxChecked = SyntaxValidating(txtCommand.Text);
            if (syntaxChecked)
            {
                new PaintCanvas(txtCommand).Show();
            }
            
           

        }

        public bool SyntaxValidating(String command) {
            Boolean test = false;
            var result = command.Split(new[] { '\r', '\n' });
            for (int j = 0; j < result.Length; j++)
            {
                string paramPattern = @"((\d+),(\d+))";
                string parampattern1 = @"(^\d+$)";
                string[] syntax = new string[] { "draw", "declare width", "counter", "+", "-", "=", "repeat", "substract", "triangle", "rectangle", "on", "cube", "polygon", "texture", "ellipse", "loop", "add", "declare", "width", "height", "x", "y", "end", "startif", "endif", "" };



                var words = result[j].ToLower().Split(' ');


                for (int i = 0; i < words.Length; i++)
                {
                    Console.WriteLine(words[i] + " " + i);
                    bool isparameterValid = Regex.IsMatch(words[i], paramPattern);
                    bool isparameterValid1 = Regex.IsMatch(words[i], parampattern1);
                    if (!isparameterValid)
                    {
                        var target = words[i];
                        var results = Array.Exists(syntax, s => s.Equals(target));
                        if (results)
                        {
                            test = true;

                        }
                        else
                        {
                            string curFile = words[i];
                            string paramPatterntest1 = @"((\d+\w),(\w\d+))";
                            string paramPatterntest2 = @"(([a-zA-Z]\d+[a-zA-Z]),([a-zA-Z]\d+[a-zA-Z]))";
                            bool isparameterValidcheck1 = Regex.IsMatch(words[i], paramPatterntest1);
                            bool isparameterValidcheck2 = Regex.IsMatch(words[i], paramPatterntest2);

                            if (isparameterValid1)
                            {
                                test = true;

                            }
                            else if (File.Exists(curFile))
                            {
                                test = true;
                            }
                            else if (isparameterValidcheck1 || isparameterValidcheck2)
                            {
                                test = false;
                                txtOutput.Text = "Invalid Parameter " + words[i];
                                return test;

                            }
                            else
                            {
                                test = false;
                                txtOutput.Text = "Invalid Command " + words[i];
                                return test;

                            }


                        }
                    }
                   
                }

            }return test;
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
           SaveFileDialog save = new SaveFileDialog();

            save.FileName = "DefaultOutputName.txt";

            save.Filter = "Text File | *.txt";

            if (save.ShowDialog() == DialogResult.OK)

            {

                StreamWriter writer = new StreamWriter(save.OpenFile());

              writer.Write(txtCommand.Text);
                
                writer.Dispose();

                writer.Close();
                MessageBox.Show("Code Sucessfully Exported");


            }
        }

        

        private void importToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,

            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtCommand.Text = File.ReadAllText(openFileDialog1.FileName, Encoding.UTF8);

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
           
        }

     
        

        private void txtCommand_TextChanged(object sender, EventArgs e)
        {
            txtOutput.Text = "";
          
            
        }

        /// <summary>
        /// action to run the code after run command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTerminal_KeyDown(object sender, KeyEventArgs e)
        {
            txtOutput.Text = "";
            if (e.KeyCode == Keys.Enter)
            {

                if (txtTerminal.Text.Equals("run"))
                {
                    
                        new PaintCanvas(txtCommand).Show();
                    
                    
                }
                else
                {
                    txtOutput.Text = "Invalid Command for terminal";
                    txtOutput.ReadOnly = true;
                }


            }
        }

        private void commandListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CommandList().Show();
        }

        /// <summary>
        /// method to check the syntax
        /// </summary>
        /// <param name="commandsyntax"></param>
        /// <returns>true or false</returns>
        public bool SyntaxChecker(String commandsyntax)
        {
            Boolean test = false;
            // string paramPattern = @"((\d+),(\d+))";
            string paramPattern = @"((\d+),(\d+))";
            string parampattern1 = @"(^\d+$)";
            string[] syntax = new string[] { "draw", "declare width", "counter", "+", "-", "=", "repeat", "substract", "triangle", "rectangle", "on", "cube", "polygon", "texture", "ellipse", "loop", "add", "declare", "width", "height", "x", "y", "end", "startif", "endif", "" };

            String command = commandsyntax.ToLower();
            //  String command = "Draw Rectangle 20,20 on x,y";
            string[] words = command.Split(' ');


            for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine(words[i] + " " + i);
                bool isparameterValid = Regex.IsMatch(words[i], paramPattern);
                bool isparameterValid1 = Regex.IsMatch(words[i], parampattern1);
                if (!isparameterValid)
                {
                    var target = words[i];
                    var results = Array.Exists(syntax, s => s.Equals(target));
                    if (results)
                    {
                        test = true;

                    }
                    else
                    {
                        string curFile = words[i];
                        string paramPatterntest1 = @"((\d+\w),(\w\d+))";
                        string paramPatterntest2 = @"(([a-zA-Z]\d+[a-zA-Z]),([a-zA-Z]\d+[a-zA-Z]))";
                        bool isparameterValidcheck1 = Regex.IsMatch(words[i], paramPatterntest1);
                        bool isparameterValidcheck2 = Regex.IsMatch(words[i], paramPatterntest2);

                        if (isparameterValid1)
                        {
                            test = true;

                        }
                        else if (File.Exists(curFile))
                        {
                            test = true;
                        }
                        else if (isparameterValidcheck1 || isparameterValidcheck2)
                        {
                            test = false;
                            txtOutput.Text = "Invalid Parameter " + words[i];
                            return test;

                        }
                        else
                        {
                            test = false;
                            txtOutput.Text = "Invalid Command " + words[i];
                            return test;

                        }


                    }
                }
                return test;
            }

            
            return test;

        }

       
    }
}
