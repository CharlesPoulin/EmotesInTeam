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
                'sky-950': '#09284C',
                'slate-600': '#30546D',
                'cyan-700': '#307089', // and '#307189', you might need to resolve if these are meant to be different
                'teal-500': '#41A0B6',
                'zinc-500': '#808080',
                'indigo-400': '#8992E5', // and '#8A93E5', same note as above
                'slate-400': '#99B0C0',
                'violet-200': '#D6DAFF',
                'red-500': '#E35B3D',
                'rose-500': '#FF2A77',
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
