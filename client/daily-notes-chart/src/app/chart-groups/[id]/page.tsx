'use client'

import { useSession } from "next-auth/react";

interface IProps{
    params: {id: string}
}

export default async function Page({params}: IProps) {
    
    const {data:session} = useSession();
    const {id} = await params

    return (
        <>
            <div>{id}</div>
            {session?.user ? (
                <div>Authed {id}</div>
            ) : (
                <div>Not Authed {id}</div>
            )}
        </>
    );
}