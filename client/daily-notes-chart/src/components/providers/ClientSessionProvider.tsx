"use client"

import { SessionProvider } from "next-auth/react";
import { ReactNode } from "react"

interface IProps{
    children: ReactNode;
    session: any;
}

export default function ClientSessionProvider({children, session}: IProps){
    return <SessionProvider session={session}>{children}</SessionProvider>
}