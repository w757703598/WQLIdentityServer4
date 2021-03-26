/// <binding BeforeBuild='min' Clean='clean' ProjectOpened='auto' />
const gulp = require('gulp'),
    cleanCss = require('gulp-clean-css'),
    less = require('gulp-less'),
    rename = require('gulp-rename'),
    concat = require("gulp-concat"),
    uglify = require("gulp-uglify"),
    del = require('del'),
    watch = require("gulp-watch"),
    concatCss = require("gulp-concat-css");


const root = "./wwwroot/";

const  node_moddules_libs= [
    {name: "jquery",dist: "./node_modules/jquery/dist/**/*.*" }, 
    {name: "bootstrap",dist:"./node_modules/bootstrap/dist/**/*.*" }
]



const paths = {
    css: root+"css/",
    lib: root + "lib/",
    js: root + "js/",
};

//css
paths.cssDist = paths.css + "**/*.css";
paths.minCssDist = paths.css + "**/*.min.css";
paths.destCssDist = paths.css + "app.min.css";

//js
paths.jsDist = paths.js + "**/*.js";
paths.minJsDist = paths.js + "**/*.min.js";
paths.destJsDist = paths.js + "app.min.js";

//清除压缩后的文件
gulp.task('clean:js', done=> {

    del([paths.minJsDist]);
    done();

});
//清除压缩后的文件
gulp.task('clean:css', done =>{

    del([paths.minCssDist]);
    done();
});

//清除
gulp.task("clean", gulp.series(["clean:js", "clean:css"]))

//移动npm到lib文件
gulp.task("move", done => {
    console.log("移动npm包");
    node_moddules_libs.forEach((item) => {
        gulp
            .src(item.dist)
            .pipe(gulp.dest(paths.lib + item.name+"/dist/"));
    })

    done();
});

//压缩


gulp.task('min:js', done=> {

    gulp
        .src([paths.jsDist, "!" + paths.minJsDist], { base: "." })
        .pipe(concat(paths.destJsDist))
        .pipe(uglify())
        .pipe(gulp.dest("."));
    done();

});
gulp.task('min:css', done=> {

    gulp
        .src([paths.cssDist, "!" + paths.minCssDist], { base: "." })
        .pipe(concat(paths.destCssDist))
        .pipe(cleanCss())
        .pipe(gulp.dest("."));
    done();

});

//清除
gulp.task("min", gulp.series(["min:js", "min:css"]))


gulp.task("auto", () => {
    watch(paths.css, gulp.series("min:css"));
    watch(paths.js, gulp.series("min:js"));
});