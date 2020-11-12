using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// src: https://www.mooict.com/c-tutorial-create-a-pong-arcade-game-in-visual-studio/
namespace Pong
{
    public partial class Pong : Form
    {
        bool goUp; // boolean to track if player is going up
        bool goDown; // boolean to track if player is going down
        int speed = 5; // starting speed of the ball
        int ballXSpeed = 5; // starting horizontal X speed value for the ball object 
        int ballYSpeed = 5; // starting vertical Y speed value for the ball object
        int playerPoints = 0; // score for the player
        int cpuPoints = 0; // score for the CPU

        public Pong()
        {
            InitializeComponent();
        }


        private void keyisup(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Up)
                {
                    // if player leaves the up key
                    // change the go up boolean to false
                    goUp = false;
                }
                if (e.KeyCode == Keys.Down)
                {
                    // if player leaves the down key
                    // change the go down boolean to false
                    goDown = false;
                }
        }
        

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                // if player presses the up key
                // change the go up boolean to true
                goUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                // if player presses the down key
                // change the go down boolean to true
                goDown = true;
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            // this is the main timer event, this event will trigger every 20 milliseconds

            playerScore.Text = "" + playerPoints; // show player score on label 1
            cpuScore.Text = "" + cpuPoints; // show CPU score on label 2

            ball.Top -= ballYSpeed; // assign the ball TOP to ball Y integer
            ball.Left -= ballXSpeed; // assign the ball LEFT to ball X integer

            cpu.Top += speed; // assignment the CPU top speed integer

            // if the score is less than 5 
            if (playerPoints < 5)
            {
                // then do the following

                // if CPU has reached the top or gone to the bottom of the screen
                if (cpu.Top < 0 || cpu.Top > 455)
                {
                    //then
                    //change the speed direction so it moves back up or down
                    speed = -speed;
                }
            }
            else
            {
                // if the score is greater than 5
                // then make the game difficult by
                // allowing the CPU follow the ball so it doesn't miss
                cpu.Top = ball.Top + 30;
            }


            //check the score
            // if the ball has gone pass the player through the left
            if (ball.Left < 0)
            {
                //then
                ball.Left = 434; // reset the ball to the middle of the screen
                ballXSpeed = -ballXSpeed; // change the balls direction
                ballXSpeed -= 2; // increase the speed
                cpuPoints++; // add 1 to the CPU score
            }

            // if the ball has gone past the right through CPU

            if (ball.Left + ball.Width > ClientSize.Width)
            {
                // then
                ball.Left = 434;  // set the ball to centre of the screen
                ballXSpeed = -ballXSpeed; // change the direction of the ball
                ballXSpeed += 2; // increase the speed of the ball
                playerPoints++; // add one to the players score
            }

            //controlling the ball
            // if the ball either reachers the top of the screen or the bottom
            if (ball.Top < 0 || ball.Top + ball.Height > ClientSize.Height)
            {
                // then
                //reverse the speed of the ball so it stays within the screen
                ballYSpeed = -ballYSpeed;
            }


            // if the ball hits the player or the CPU
            if (ball.Bounds.IntersectsWith(player.Bounds) || ball.Bounds.IntersectsWith(cpu.Bounds))
            {
                // then bounce the ball in the other direction
                ballXSpeed = -ballXSpeed;
            }

            // controlling the player

            // if go up boolean is true and player is within the top boundary
            if (goUp == true && player.Top > 0)
            {
                // then
                // move player towards to the top
                player.Top -= 8;
            }

            // if go down boolean is true and player is within the bottom boundary
            if (goDown == true && player.Top < 455)
            {
                // then
                // move player towards the bottom of screen
                player.Top += 8;
            }

            // final score and ending the game
            // if the player score more than 10
            // then we will stop the timer and show a message box
            if (playerPoints > 10)
            {
                gameTimer.Stop();
                MessageBox.Show("You win this game");
            }
            // if the CPU scores more than 10
            // then we will stop the timer and show a message box
            if (cpuPoints > 10)
            {
                gameTimer.Stop();
                MessageBox.Show("CPU wins, you lose");
            }
        }
    }
}
