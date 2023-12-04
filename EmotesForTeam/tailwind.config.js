/** @type {import('tailwindcss').Config} */
const colors = require('tailwindcss/colors')

module.exports = {
  content: ['./**/*.html', './**/*.razor'],
  theme: {
      extend: {},
      colors: {
          // Build your palette here
          transparent: 'transparent',
          current: 'currentColor',
          gray: colors.trueGray,
          red: colors.red,
          blue: colors.sky,
          yellow: colors.amber,
      },
  },
  plugins: [],
}

