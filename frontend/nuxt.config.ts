// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2025-05-15',
  devtools: { enabled: true },

  modules: [
    '@nuxt/eslint',
    '@nuxtjs/tailwindcss',
  ],

  css: [
    '~/assets/css/tailwind.css',
  ],

  tailwindcss: {
    configPath: '~/tailwind.config.js',
  },

  runtimeConfig: {
    public: {
      apiBase: '/api'
    }
  },

  nitro: {
    devProxy: {
      '/api': {
        target: 'http://localhost:5095',
        changeOrigin: true
      }
    }
  },

  app: {
    head: {
      title: 'AntiRizzGPT',
      meta: [
        { charset: 'utf-8' },
        { name: 'viewport', content: 'width=device-width, initial-scale=1' },
        { name: 'description', content: 'Test your rizz lines and get instant anti-rizz' }
      ],
      link: [
        { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }
      ]
    }
  }
})
