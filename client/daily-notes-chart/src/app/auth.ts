import NextAuth from "next-auth"
import Credentials from "next-auth/providers/credentials"
 
export const { handlers, signIn, signOut, auth } = NextAuth({
  providers: [
    Credentials({
      // You can specify which fields should be submitted, by adding keys to the `credentials` object.
      // e.g. domain, username, password, 2FA token, etc.
      credentials: {
        email: {},
        username: {},
        password: {},
      },
      authorize: async (credentials) => {
        console.log("authorize: "+JSON.stringify(credentials))
        var { username, email, password } = credentials as any;

        const response = await fetch("https://localhost:7085/api/account/loginByUserName", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                username,
                password,
            })
        });

        if(!response.ok){
            throw new Error("Invalid credentials.")
        }

        const user = await response.json();

        if (user) {
            return user;
        } else {
            return null;
        }
      },
    })
  ],
})