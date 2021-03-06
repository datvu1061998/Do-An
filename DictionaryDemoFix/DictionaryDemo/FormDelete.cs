﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DictionaryDemo
{
    public partial class FormDelete : Form
    {
        private MainScreen _frmMainScreen;

        public FormDelete(Form frmMainScreen)
        {
            _frmMainScreen = (MainScreen) frmMainScreen;
            InitializeComponent();
        }

        private void txt_DelName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_Del.Focus();
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            string s1 = txt_DelName.Text;
            try
            {
                if (s1 == "" || s1 == null)
                    throw new Exception("You have to fill enough word");
                bool check = _frmMainScreen.BB.Search_Check(s1);
                if (check == false)
                    MessageBox.Show("This word is not in this Dictionary", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                else
                {
                    //Word wd = new Word(s1, s2,null);
                    _frmMainScreen.BB.RemoveWord(s1);
                    MessageBox.Show("This Word has been Delete", "Congratulation", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    _frmMainScreen.comboBox1.Items.Remove(s1);

                    for (int i = 0; i < _frmMainScreen.DS.Count; i++)
                    {
                        if (_frmMainScreen.DS[i].Name == s1)
                            _frmMainScreen.DS.RemoveAt(i);
                    }

                    //_frmMainScreen.comboBox1.Items.Clear();
                    //MainScreen.AddComboBox(_frmMainScreen.BB, _frmMainScreen.comboBox1);

                    FileStream file = new FileStream(
                        Link.filepath,
                        FileMode.Create, FileAccess.Write);
                    StreamWriter write = new StreamWriter(file);

                    for (int i = 0; i < _frmMainScreen.DS.Count; i++)
                    {
                        string k = _frmMainScreen.DS[i].Name + '@' + _frmMainScreen.DS[i].Meaning + '@' +
                                   _frmMainScreen.DS[i].Explication;
                        write.WriteLine(k);
                    }
                    write.Flush();
                    write.Close();
                    file.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
