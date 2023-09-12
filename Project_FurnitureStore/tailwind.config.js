module.exports = {
    content: [
        './Pages/**/*.cshtml',
        './Views/**/*.cshtml'
    ],
    theme: {
        extend: {
            colors: {
                code1: 'rgb(154, 59, 59)',
                code2: 'rgb(192, 130, 97)',
                code3: 'rgb(226, 199, 153)',
                code4: 'rgb(242, 236, 190)',

                code5Hot: 'rgb(253, 141, 20)',
            },
            fontFamily: {
                DancingScript: ['Dancing Script'],
                Raleway: ['Raleway']
            }

        },
    },
    plugins: [],
}