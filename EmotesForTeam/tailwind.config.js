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
                ...colors, // Spread all Tailwind's default colors
                'sky-950': '#09284C',
                'slate-600': '#30546D',
                'cyan-700': '#307089',
                'teal-500': '#41A0B6',
                'zinc-500': '#808080',
                'indigo-400': '#8992E5',
                'slate-400': '#99B0C0',
                'violet-200': '#D6DAFF',
                'red-500': '#E35B3D',
                'rose-500': '#FF2A77',
                'orange-100': '#fccfa6',
                neutral: {
                    100: '#f5f5f5',
                },
                indigo: {
                    50: '#eef2ff',
                },
            },
        },
    },
    plugins: [],
    mode: 'jit',
    purge: ['./**/*.html', './**/*.razor'],
}
