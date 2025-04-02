declare module "next-auth" {
    interface Session {
        user: {
            userId: string,
            userName: string,
            userEmail: string,
            roles: string[];
        };
    }

    interface User {
        userId: string,
        userName: string,
        userEmail: string,
        roles: string[];
    }
}