# Installing

To use cache busting you need to do the following

## .NET Framework
nuget : ```Install-Package Gibe.CacheBusting```

## .NET Core
```dotnet add package Gibe.CacheBusting```

## Gulp

You will need to add [gulp-rev](https://www.npmjs.com/package/gulp-rev"gulp-rev"), [gulp-rev-delete-original](https://www.npmjs.com/package/gulp-rev-delete-original"gulp-rev-delete-original") and [gulp-filter](https://www.npmjs.com/package/gulp-filter"gulp-filter") to your package.json

Update the gulp file to then include rev(), revdel() and rev.manifest() in the process for building css and js

For example:

```javascript
var gulp = require('gulp'),
	bower = require('gulp-bower'),
	sass = require('gulp-sass'),
	sourcemaps = require('gulp-sourcemaps'),
	concat = require('gulp-concat'),
	uglify = require('gulp-uglify'),
	rev = require('gulp-rev'),
	revdel = require('gulp-rev-delete-original'),
	filter = require('gulp-filter');

gulp.task('css', function () {
	return gulp.src('./gulp/scss/**/*.scss')
		.pipe(sourcemaps.init())
		.pipe(sass({ outputStyle: 'compressed' }))
		.pipe(sourcemaps.write('.'))
		.pipe(gulp.dest('./css'))
		.pipe(filter('**/*.css'))
		.pipe(rev())
		.pipe(revdel())
		.pipe(gulp.dest('./css'))
		.pipe(rev.manifest())
		.pipe(gulp.dest('./css'));
});

gulp.task('js', function () {
	return gulp.src([
			'./gulp/scripts/scripts-lib/**/*.js',
			'./gulp/scripts/history.js/history.js',
			'./gulp/scripts/history.js/history.adapter.jquery.js',
			'./gulp/scripts/plugins/**/*.js',
			'./gulp/scripts/site.js',
			'./gulp/scripts/modules/**/*.js'
		])
		.pipe(sourcemaps.init())
		.pipe(concat('site.js'))
		.pipe(uglify())
		.pipe(sourcemaps.write('.'))
		.pipe(gulp.dest('./js'))
		.pipe(filter('**/*.js'))
		.pipe(rev())
		.pipe(revdel())
		.pipe(gulp.dest('./js'))
		.pipe(rev.manifest())
		.pipe(gulp.dest('./js'));
});

gulp.task('build', ['css', 'js']);

gulp.task('watch', function () {
	gulp.watch('./gulp/scss/**/*.scss', ['css']);
	gulp.watch('./gulp/scripts/**/*.js', ['js']);
});
```
## .NET Framework configuration

Then you can add to web.config `<configSections>`
```xml
<section name="cacheBusting" type="Gibe.CacheBusting.Config.CacheBustingSection, Gibe.CacheBusting" />
```
And add this section somewhere in `<configuration>` in your web.config

```xml
<cacheBusting>
	<manifests>
		<add path="/css/" file="~/css/rev-manifest.json" />
		<add path="/js/" file="~/js/rev-manifest.json" />
	</manifests>
</cacheBusting>
```

Add to your ~/views/web.config : ```xml <add namespace="Gibe.CacheBusting" />```

## .NET Core

```services.AddCacheBusting```

Add to Application.json : 
```
"CacheBusting": 
{
	"/css/": "/css/rev-manifest.json",
	"/js/": "/js/rev-manifest.json"
}
```

## Using in views

Use in your views ```@Url.Asset("/js/site.js")```

This will automatically change the URL to the hashed version if found in the manifest.
