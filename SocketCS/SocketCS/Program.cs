using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using NKH.MindSqualls;

namespace TestNetworkServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Char delimiter = ',';
            int rotate_count = 0;
            int catch_flag = 0;
            int arm_flag = 0;
            int detect_flag = 0;
            NxtBluetoothConnection conn;
            conn = new NxtBluetoothConnection(6);
            conn.Connect();

            System.Net.IPAddress theIPAddress;
            //建立 IPAddress 物件(本機)
            theIPAddress = System.Net.IPAddress.Parse("127.0.0.1");

            //建立監聽物件
            TcpListener myTcpListener = new TcpListener(theIPAddress, 36000);
            //啟動監聽
            myTcpListener.Start();
            Console.WriteLine("通訊埠 36000 等待用戶端連線...... !!");
            Socket mySocket = myTcpListener.AcceptSocket();
            do
            {
                try
                {
                    //偵測是否有來自用戶端的連線要求，若是
                    //用戶端請求連線成功，就會秀出訊息。
                    if (mySocket.Connected)
                    {
                        int dataLength;
                        double d = 45;
                        int d_x = 300;
                        double temp_d = 0;
                        int temp_d_x = 0;
                        int count = 0;
                        
                        Console.WriteLine("連線成功 !!");
                        byte[] myBufferBytes = new byte[1000];
                        //取得用戶端寫入的資料
                        dataLength = mySocket.Receive(myBufferBytes);

                        Console.WriteLine("接收到的資料長度 {0} \n ", dataLength.ToString());
                        Console.WriteLine("取出用戶端寫入網路資料流的資料內容 :");
                        //因為接收是byte需要轉成String
                        string message= Encoding.ASCII.GetString(myBufferBytes, 0, dataLength) + "\n";
                        Console.WriteLine(message);
                        string copy_message = String.Copy(message);
                        String[] substrings = copy_message.Split(delimiter);
                        foreach (var substring in substrings)
                        {
                            //Console.WriteLine(substring);
                            if (count == 0)
                            {
                                temp_d = Double.Parse(substring);
                            }
                            if (count == 1)
                            {
                                temp_d_x = Int32.Parse(substring);
                            }
                            count++;
                        }
                        //Console.WriteLine("{0}, {1}",temp_d,temp_d_x);


                        if(catch_flag == 0)
                        {
                            /**********************************FORWARD*********************************/
                            if (temp_d > d && temp_d_x <= d_x + 10 && temp_d_x >= d_x - 10)
                            {
                                conn.StartProgram("forward.rxe");
                                System.Threading.Thread.Sleep(300);
                            }
                            /*********************************BIGFORWARD********************************/
                            else if (temp_d > 100 && temp_d_x <= d_x + 20 && temp_d_x >= d_x - 20)
                            {
                                conn.StartProgram("bigforward.rxe");
                                System.Threading.Thread.Sleep(1100);
                            }
                            /***********************************RIGHT**********************************/
                            else if (temp_d_x > d_x + 10 && temp_d < 600 && temp_d > 0)
                            {
                                if (temp_d <= 100)
                                {
                                    conn.StartProgram("right.rxe");
                                    System.Threading.Thread.Sleep(150);
                                }
                                else if (temp_d_x > d_x + 20 && temp_d > 100)
                                {
                                    conn.StartProgram("right.rxe");
                                    System.Threading.Thread.Sleep(150);
                                }
                            }
                            /***************************LEFT********************************************/
                            else if (temp_d_x < d_x - 10 && temp_d < 600 && temp_d > 0)
                            {
                                if (temp_d <= 100)
                                {
                                    conn.StartProgram("left.rxe");
                                    System.Threading.Thread.Sleep(150);
                                }
                                else if (temp_d_x < d_x - 20 && temp_d > 100)
                                {
                                    conn.StartProgram("left.rxe");
                                    System.Threading.Thread.Sleep(150);
                                }
                            }
                            /************************************GRAB**********************************/
                            else if (temp_d <= d && temp_d_x <= d_x + 10 && temp_d_x >= d_x - 10 && temp_d > 0)
                            {
                                if (arm_flag == 0)
                                {
                                    conn.StartProgram("total_arm_get.rxe");
                                    System.Threading.Thread.Sleep(3600);
                                    arm_flag = 1;
                                }
                                else if (arm_flag == 1 && temp_d > d - 7)
                                {
                                    conn.StartProgram("reach.rxe");
                                    System.Threading.Thread.Sleep(300);
                                }
                                else if (arm_flag == 1 && temp_d <= d - 7)
                                {
                                    conn.StopProgram();
                                    System.Threading.Thread.Sleep(3000);
                                    conn.StartProgram("hand_get.rxe");
                                    System.Threading.Thread.Sleep(3500);
                                    catch_flag = 1;
                                    conn.StartProgram("total_arm_reset.rxe");
                                    System.Threading.Thread.Sleep(3600);
                                    arm_flag = 0;
                                    conn.StartProgram("turn_back.rxe");
                                    System.Threading.Thread.Sleep(300);

                                }
                            }
                            else if(temp_d == -1 && temp_d_x == 0)
                            {
                                conn.StartProgram("rotate45.rxe");
                                System.Threading.Thread.Sleep(650);
                            }
                        }

                        else if(catch_flag == 1)
                        {
                            /**********************************FORWARD*********************************/
                            if (temp_d > d + 85 && temp_d_x <= d_x + 10 && temp_d_x >= d_x - 10)
                            {
                                conn.StartProgram("forward_back.rxe");
                                System.Threading.Thread.Sleep(300);
                            }
                            /*********************************BIGFORWARD********************************/
                            else if (temp_d > d + 130 && temp_d_x <= d_x + 20 && temp_d_x >= d_x - 20)
                            {
                                conn.StartProgram("bigforward_back.rxe");
                                System.Threading.Thread.Sleep(1100);
                            }
                            /***********************************RIGHT**********************************/
                            else if (temp_d_x > d_x + 10 && temp_d < 1000 && temp_d > d + 85)
                            {
                                if (temp_d <= d + 130)
                                {
                                    conn.StartProgram("right_back.rxe");
                                    System.Threading.Thread.Sleep(150);
                                }
                                else if (temp_d_x > d_x + 20 && temp_d > d + 130)
                                {
                                    conn.StartProgram("right_back.rxe");
                                    System.Threading.Thread.Sleep(150);
                                }
                            }
                            /***************************LEFT********************************************/
                            else if (temp_d_x < d_x - 10 && temp_d < 1000 && temp_d > d + 85)
                            {
                                if (temp_d <= d + 130)
                                {
                                    conn.StartProgram("left_back.rxe");
                                    System.Threading.Thread.Sleep(150);
                                }
                                else if (temp_d_x < d_x - 20 && temp_d > d + 130)
                                {
                                    conn.StartProgram("left_back.rxe");
                                    System.Threading.Thread.Sleep(150);
                                }
                            }
                            /*****************************************DETECT****************************/
                            else if (temp_d <= d + 85 && temp_d_x <= d_x + 10 && temp_d_x >= d_x - 10 && temp_d != -4)
                            {
                                detect_flag = 1;
                            }
                            /**************************************PUT*********************************/
                            else if (detect_flag == 1 && temp_d == -4)
                            {
                                if (temp_d_x == -1)
                                {
                                    conn.StartProgram("left_back_put.rxe");
                                    System.Threading.Thread.Sleep(1000);
                                    conn.StopProgram();
                                    System.Threading.Thread.Sleep(3000);
                                    conn.StartProgram("forward_put.rxe");
                                    System.Threading.Thread.Sleep(1800);
                                    conn.StartProgram("total_arm_get.rxe");
                                    System.Threading.Thread.Sleep(3600);

                                    conn.StartProgram("hand_reset.rxe");
                                    System.Threading.Thread.Sleep(3500);                                    
                                    conn.StartProgram("total_arm_reset.rxe");
                                    System.Threading.Thread.Sleep(3600);

                                    conn.StartProgram("backward_put.rxe");
                                    System.Threading.Thread.Sleep(1800);
                                    conn.StartProgram("right_back_put.rxe");
                                    System.Threading.Thread.Sleep(1000);
                                    conn.StartProgram("turn_back.rxe");
                                    System.Threading.Thread.Sleep(300);
                                    detect_flag = 0;
                                    catch_flag = 0;
                                }
                                else if (temp_d_x == 0)
                                {
                                    conn.StartProgram("forward_put.rxe");
                                    System.Threading.Thread.Sleep(1800);
                                    conn.StopProgram();
                                    System.Threading.Thread.Sleep(3000);
                                    conn.StartProgram("total_arm_get.rxe");
                                    System.Threading.Thread.Sleep(3600);

                                    conn.StartProgram("hand_reset.rxe");
                                    System.Threading.Thread.Sleep(3500);                                    
                                    conn.StartProgram("total_arm_reset.rxe");
                                    System.Threading.Thread.Sleep(3600);

                                    conn.StartProgram("backward_put.rxe");
                                    System.Threading.Thread.Sleep(1800);
                                    conn.StartProgram("turn_back.rxe");
                                    System.Threading.Thread.Sleep(300);
                                    detect_flag = 0;
                                    catch_flag = 0;
                                }
                                else if (temp_d_x == 1)
                                {
                                    conn.StartProgram("right_back_put.rxe");
                                    System.Threading.Thread.Sleep(1000);
                                    conn.StopProgram();
                                    System.Threading.Thread.Sleep(3000);
                                    conn.StartProgram("forward_put.rxe");
                                    System.Threading.Thread.Sleep(1800);
                                    conn.StartProgram("total_arm_get.rxe");
                                    System.Threading.Thread.Sleep(3600);

                                    conn.StartProgram("hand_reset.rxe");
                                    System.Threading.Thread.Sleep(3500);                                    
                                    conn.StartProgram("total_arm_reset.rxe");
                                    System.Threading.Thread.Sleep(3600);

                                    conn.StartProgram("backward_put.rxe");
                                    System.Threading.Thread.Sleep(1800);
                                    conn.StartProgram("left_back_put.rxe");
                                    System.Threading.Thread.Sleep(1000);
                                    conn.StartProgram("turn_back.rxe");
                                    System.Threading.Thread.Sleep(300);
                                    detect_flag = 0;
                                    catch_flag = 0;
                                }
                            }
                            else if (temp_d == -1 && temp_d_x == 0)
                            {
                                conn.StartProgram("rotate45.rxe");
                                System.Threading.Thread.Sleep(650);
                            }
                        }

                        /*else if(temp_d == -1)
                        {
                            if(rotate_count >= 3)
                            {
                                conn.StartProgram("rotate90.rxe");
                                System.Threading.Thread.Sleep(1050);
                                rotate_count = 0;
                            }
                            else
                            {
                                conn.StartProgram("bigforward.rxe");
                                System.Threading.Thread.Sleep(1100);
                                rotate_count++;
                            }                            
                        }*/
                        //cur_x = temp_d_x;
                        //Console.WriteLine("按下 [任意鍵] 將資料回傳至用戶端 !!");
                        //Console.ReadLine();
                        //將接收到的資料回傳給用戶端
                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    //mySocket.Close();
                    //break;
                }

            } while (true);
        }
    }
}