module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
    "./public/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      colors:{
        primary:{
          lighter: '#3DBFFF',
          light: '#2BAAE9',
          DEFAULT: '#068CCE',
          dark: '#0073AC',
        },
        secondary:{
          light: '#2F4A77',
          DEFAULT: '#132A4F',
          dark: '#051329',
        },
        danger:{
          light: '#FFCAC7',
          DEFAULT: '#FF4C41',
          accent: '#C8170C',
        },
        success:{
          light: '#D1F6D0',
          DEFAULT: '#4AB547',
          dark: '#038B00',
        },
        warn:{
          light: '#FFE4AF',
          DEFAULT: '#FFBE41',
          accent: '#FBA500',
        },
        info:{
          light: '#E1C3FA',
          DEFAULT: '#8547B5',
          accent: '#590F93',
        },
        gray:{
          "100": '#E2E9ED',
          "200": '#9BBFD1',
          "300": '#383F4A',
          "400": '#2C394D',
          "500": '#272B33',
          "600": '#18212F',
          "700": '#101926', 
          "800": '#0F1621',
          "900": '#0A121E',
          "950": '#0C111B',
        },
      },
      fontFamily: {
        "body": ["Poppins", "sans-serif"]
      }
    },
  },
  plugins: [
    require('@tailwindcss/forms')({
      strategy: 'class'
    }),
  ],
}