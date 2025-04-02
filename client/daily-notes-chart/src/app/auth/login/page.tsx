"use client"

import React, { useState } from 'react';
import styles from './page.module.css'
import authStyles from '../auth.module.css'
import { signIn } from 'next-auth/react';
import IError from '../../../../utils/interfaces/IError';

export default function Page() {
    const [emailOrUserName, setEmailOrUserName] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");


    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        setError("");

        const result = await signIn("credentials", {
            emailOrUserName: emailOrUserName, 
            password: password,
            redirect: false,
            callbackUrl: '/',
        })

        if(result?.error){
            try{
                const errorData = JSON.parse(result.error) as IError[];
                setError(errorData[0].message);
            } catch {
                setError(result.error)
            }
        }
    }

    return (
        <div className='centered'>
            <form
                className={authStyles.loginForm}
                onSubmit={handleSubmit}
            >
                <h2 className={authStyles.title}>Login</h2>

                <small className={authStyles.inputTitle}>User name or Email</small>
                <input
                    className={authStyles.input}
                    value={emailOrUserName}
                    placeholder='Enter user name or email...'
                    onChange={(e) => setEmailOrUserName(e.target.value)}
                /> <br />

                <small className={authStyles.inputTitle}>Password</small>
                <input
                    className={authStyles.input}
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    placeholder='Enter password...'
                /> <br />

                {error && <label className={styles.errorMessage}>{error}</label>}

                <button className='btn accent1' type="submit">Login</button>
            </form>
        </div>
    );
}