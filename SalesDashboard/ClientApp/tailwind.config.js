/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "../Views/**/*.cshtml",
        "../Pages/**/*.cshtml",      // (در صورت وجود Razor Pages)
        "../Areas/**/*.cshtml",      // (در صورت وجود Area)
        "../wwwroot/js/**/*.js"      // (در صورت استفاده از فایل‌های JS)
    ],
    theme: {
        extend: {},
    },
    plugins: [],
}
