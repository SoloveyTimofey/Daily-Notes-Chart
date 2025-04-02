'use client'

import { signIn, signOut, useSession } from "next-auth/react";
import styles from './header.module.css'

export default function Header() {

    const { data: session } = useSession();

    console.log("session: " + JSON.stringify(session));
    return (
        <div className={styles.container}>
            {session?.user ? (
                <>
                    <p>{session.user.userName}</p>
                    <button onClick={() => signOut()}>
                        Sign Out
                    </button>
                    <button onClick={async ()=>{
                        const res = await fetch("https://localhost:7085/api/chartGroups", {
                            method: "GET",
                            headers:{
                                authorization: `Bearer ${session?.user.token}`,
                            }
                        })

                        console.log("Res: "+ JSON.stringify(res))
                    }}>
                        POST
                    </button>
                </>
            ) : (
                <>
                    <button onClick={() => signIn()}>
                        Sign In
                    </button>
                </>
            )}
        </div>
    );
}