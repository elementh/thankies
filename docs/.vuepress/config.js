module.exports = {
    base: '/',
    head: [['link', { rel: 'icon', href: '/images/favicon.png' }]],
    title: 'Thankies',
    description: 'Be grateful in telegram!',
    themeConfig: {
        logo: '/images/logo.png',
        darkMode: false,
        navbar: [
        ],
        repo: 'elementh/thankies'
    },
    plugins: [
        '@vuepress/back-to-top'
    ]
}