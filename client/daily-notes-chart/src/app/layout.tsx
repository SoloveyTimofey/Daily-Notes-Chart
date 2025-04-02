"use client"

import { ReactNode } from "react";
import Header from '@/components/headers/header'
import '@/styles/globals.css'
import { Silkscreen, Roboto, Montserrat } from "next/font/google"
import { SessionProvider } from "next-auth/react";

export const silkscreen = Silkscreen({
  subsets: ['latin'],
  weight: ['400', '700'],
  variable: '--font-silkscreen'
});

export const roboto = Roboto({
  subsets: ['latin'],
  weight: ['200', '300', '400', '500', '600', '700', '800', '900'],
  variable: '--font-roboto',
});

export const montserrat = Montserrat({
  weight: ['100', '200', '300', '400', '500', '600', '700', '800'],
  variable: '--font-montserrat'
});


interface IProps {
  children: ReactNode;
  session: any;
}

export default function RootLayout({ children, session }: IProps) {
  return (
    <html lang="en">
      <body className={`${silkscreen.variable} ${roboto.variable} ${montserrat.variable}`}>
        <SessionProvider session={session}>
          <Header />
          {children}
        </SessionProvider>
      </body>
    </html>
  );
}
