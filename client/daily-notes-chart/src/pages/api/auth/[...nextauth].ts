import CredentialsProvider from "next-auth/providers/credentials";
import { Session } from "next-auth";
import NextAuth from "next-auth/next"
import { JWT } from "next-auth/jwt";
import { json } from "stream/consumers";
import { isEmail } from "@/lib/regexChecks";

// process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";

export const authOptions: any = {
    // Configure one or more authentication providers
    providers: [
        // ...add more providers here
        CredentialsProvider({
            // The name to display on the sign in form (e.g. "Sign in with...")
            name: "Credentials",
            // `credentials` is used to generate a form on the sign in page.
            // You can specify which fields should be submitted, by adding keys to the `credentials` object.
            // e.g. domain, username, password, 2FA token, etc.
            // You can pass any HTML attribute to the <input> tag through the object.
            credentials: {
                password: {
                    label: "Password",
                    type: "password",
                },
                emailOrUserName: {
                    label: "Email or User name",
                    type: "text",
                }
            },
            async authorize(credentials, req): Promise<any> {
                const { emailOrUserName, password } = credentials as any;

                console.log('Next Auth authorize called!');

                var endpointName;
                var body;
                if(isEmail(emailOrUserName)) {
                    endpointName = "loginByEmail"
                    body = {
                        email: emailOrUserName,
                        password: password,
                    }
                } else {
                    endpointName = "loginByUserName"
                    body = {
                        userName: emailOrUserName,
                        password: password,
                    }
                }

                const res = await fetch(`https://localhost:7085/api/account/${endpointName}`, {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(body),
                });

                const result = await res.json();

                if (res.ok && result) {
                    return result;
                }

                // Pass the error to the component
                throw new Error(JSON.stringify(result))
            },
        })
    ],

    callbacks: { // Define these functions to write the token into the session
        async jwt({token, user}){
            return {...token, ...user}
        },

        async session({session, token, user}){
            session.user = token;
            return session;
        }
    },


    session: {
        strategy: "jwt"
    },

    pages: {
        signIn: "/auth/login",
    },
};

export default NextAuth(authOptions);