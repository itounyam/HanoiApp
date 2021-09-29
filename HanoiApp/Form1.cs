using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HanoiApp
{
    public partial class Form1 : Form
    {
        const int STANDARD_WARKA_LOCATION_X = 30;
        const int STANDARD_WARKA_LOCATION_Y = 45;
        const int NEXT_LOCATION_X_ADD_NUM = 70;
        const int NEXT_LOCATION_Y_ADD_NUM = 20;
        const string INTERVAL_ROUTE = "//hanoi-config/time/move-num-interval";

        ConfigGetter thisConfigGetter;
        int _stageLength;
        List<Label> _workA;
        List<Label> _workB;
        List<Label> _workC;
        int _moveNum;
        int _moveInterval;

        public Form1()
        {
            InitializeComponent();

            thisConfigGetter = new ConfigGetter();
            _stageLength = 0;
            _workA = new List<Label>();
            _workB = new List<Label>();
            _workC = new List<Label>();
            _moveInterval = thisConfigGetter.GetIntdata(INTERVAL_ROUTE);
        }

        private void hanoi(int num, char a, char b, char c)
        {
            if (num > 0)
            {
                hanoi(num - 1, a, c, b);
                moveNum(a, b);
                hanoi(num - 1, c, b, a);
            }
        }

        private void moveNum(char a, char b)
        {
            switch(a)
            {
                case 'a':
                    setMoveNum(extractNumForWarkA());
                    break;
                case 'b':
                    setMoveNum(extractNumForWarkB());
                    break;
                case 'c':
                    setMoveNum(extractNumForWarkC());
                    break;

            }

            switch (b)
            {
                case 'a':
                    moveNumToWarkA();
                    break;
                case 'b':
                    moveNumToWarkB();
                    break;
                case 'c':
                    moveNumToWarkC();
                    break;

            }
            this.Refresh();
            System.Threading.Thread.Sleep(_moveInterval);
        }

        private void setMoveNum(int num)
        {
            _moveNum = num;
        }

        private int getMoveNum()
        {
            return _moveNum;
        }

        private int extractNumForWarkA()
        {
            int i = 0;
            while (_workA[i].Text == "")
            {
                i++;
            }
            int num = Int32.Parse(_workA[i].Text);
            _workA[i].Text = "";
            return num;
        }

        private int extractNumForWarkB()
        {
            int i = 0;
            while (_workB[i].Text == "")
            {
                i++;
            }
            int num = Int32.Parse(_workB[i].Text);
            _workB[i].Text = "";
            return num;
        }

        private int extractNumForWarkC()
        {
            int i = 0;
            while (_workC[i].Text == "")
            {
                i++;
            }
            int num = Int32.Parse(_workC[i].Text);
            _workC[i].Text = "";
            return num;
        }

        private void moveNumToWarkA()
        {
            int i = 0;
            while (_workA[i].Text == "")
            {
                i++;
                if(i >= getStageLength())
                {
                    break;
                }
            }
            _workA[i - 1].Text = getMoveNum().ToString();
        }

        private void moveNumToWarkB()
        {
            int i = 0;
            while (_workB[i].Text == "")
            {
                i++;
                if (i >= getStageLength())
                {
                    break;
                }
            }
            _workB[i - 1].Text = getMoveNum().ToString();
        }

        private void moveNumToWarkC()
        {
            int i = 0;
            while (_workC[i].Text == "")
            {
                i++;
                if (i >= getStageLength())
                {
                    break;
                }
            }
            _workC[i - 1].Text = getMoveNum().ToString();
        }

        private void stageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.resetButton.Enabled = true;
            this.startButton.Enabled = true;
            this.stageComboBox.Enabled = false;
            setStageLength(Int32.Parse(stageComboBox.SelectedItem.ToString()));
            setWorks();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            this.resetButton.Enabled = false;
            this.startButton.Enabled = false;
            this.stageComboBox.Enabled = true;
            resetWorks();
        }

        private void resetWorks()
        {
            for (int i = 0; i < _stageLength; i++)
            {
                this.Controls.Remove(_workA[i]);
                this.Controls.Remove(_workB[i]);
                this.Controls.Remove(_workC[i]);
            }
            _workA.Clear();
            _workB.Clear();
            _workC.Clear();
        }

        private void setStageLength(int num)
        {
            _stageLength = num;
        }

        private int getStageLength()
        {
            return _stageLength;
        }
        private void setWorks()
        {
            int psitionX = STANDARD_WARKA_LOCATION_X;
            int psitionY = STANDARD_WARKA_LOCATION_Y;
            for (int i = 0; i < getStageLength(); i++)
            {
                Label workALabel = new Label();
                workALabel.AutoSize = true;
                workALabel.Location = new System.Drawing.Point(psitionX, psitionY);
                workALabel.Name = "workA_label" + i.ToString();
                workALabel.Size = new System.Drawing.Size(64, 15);
                workALabel.TabIndex = 3+i;

                Label workBLabel = new Label();
                workBLabel.AutoSize = true;
                workBLabel.Location = new System.Drawing.Point(psitionX + NEXT_LOCATION_X_ADD_NUM, psitionY);
                workBLabel.Name = "workB_label" + i.ToString();
                workBLabel.Size = new System.Drawing.Size(64, 15);
                workBLabel.TabIndex = 3 + i + getStageLength();

                Label workCLabel = new Label();
                workCLabel.AutoSize = true;
                workCLabel.Location = new System.Drawing.Point(psitionX + NEXT_LOCATION_X_ADD_NUM * 2, psitionY);
                workCLabel.Name = "workC_label" + i.ToString();
                workCLabel.Size = new System.Drawing.Size(64, 15);
                workCLabel.TabIndex = 3 + i + getStageLength() * 2;

                _workA.Add(workALabel);
                 int textNum = i +1;
                _workA[i].Text = textNum.ToString();
                _workB.Add(workBLabel);
                _workB[i].Text = "";
                _workC.Add(workCLabel);
                _workC[i].Text = "";

                this.Controls.Add(this._workA[i]);
                this.Controls.Add(this._workB[i]);
                this.Controls.Add(this._workC[i]);

                psitionY += NEXT_LOCATION_Y_ADD_NUM;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.resetButton.Enabled = false;
            this.startButton.Enabled = false;
            char a = 'a';
            char b = 'b';
            char c = 'c';
            hanoi(getStageLength(), a, b, c);
            this.resetButton.Enabled = true;
        }

    }
}
