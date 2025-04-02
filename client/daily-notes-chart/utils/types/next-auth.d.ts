import NextAuth from "next-auth";

declare module "next-auth" {
    interface Session{
        user: {
            userId: string,
            userName: string,
            userEmail: string,
            roles: string[],
            token: string,
            refreshToken: string,
        },
        expires: string
    }
}