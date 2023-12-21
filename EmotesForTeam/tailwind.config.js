/** @type {import('tailwindcss').Config} */
const colors = require('tailwindcss/colors')

module.exports = {
    content: ['./**/*.html', './**/*.razor'],
    theme: {
        extend: {
            fontFamily: {
                'sans': ['Roboto', 'sans-serif'],
            },
            colors: {
                neutral: {
                    100: '#f5f5f5',
                },
                indigo: {
                    50: '#eef2ff',
                },
            },
        },
        colors: {
            ...colors,
            transparent: 'transparent',
            current: 'currentColor',
        },
    },
    plugins: [],
    mode: 'jit',
    purge: ['./**/*.html', './**/*.razor'],
}
