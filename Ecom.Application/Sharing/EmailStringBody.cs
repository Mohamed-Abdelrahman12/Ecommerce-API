namespace Ecom.Application.Sharing
{
    public class EmailStringBody
    {
        public static string Send(string email, string token, string component, string message)
        {
            string encodeToken = Uri.EscapeDataString(token);

            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='UTF-8'>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 20px auto;
                            background: #ffffff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
                        }}
                        h2 {{
                            color: #333333;
                        }}
                        p {{
                            color: #555555;
                            line-height: 1.5;
                        }}
                        a.button {{
                            display: inline-block;
                            margin-top: 20px;
                            padding: 10px 20px;
                            background-color: #007BFF;
                            color: #ffffff !important;
                            text-decoration: none;
                            border-radius: 5px;
                        }}
                        .footer {{
                            margin-top: 30px;
                            font-size: 12px;
                            color: #888888;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2>Hello {email},</h2>
                        <p>{message}</p>
                        <p>Click the button below to continue with {component}:</p>
                        <a class='button' href='http://localhost:4200/{component}?email={email}&code={encodeToken}'>Continue</a>
                        <div class='footer'>
                            <p>If you didn’t request this, you can ignore this email.</p>
                        </div>
                    </div>
                </body>
                </html>
            ";
        }
    }
}
