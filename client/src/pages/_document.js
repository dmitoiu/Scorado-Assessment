import React from "react";
import { Html, Head, Main, NextScript } from 'next/document'

export default function Document() {
    return (
        <Html className={"h-full bg-gray-800"}>
            <Head />
            <body className={"h-full bg-gray-800"}>
            <Main />
            <NextScript />
            </body>
        </Html>
    )
}