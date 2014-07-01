# Environment Setup

* Install [Chocolatey](http://chocolatey.org/)
* Install Ruby 1.9, 7-Zip, and Ruby DevKit

```
# Install Ruby 1.9
> cinst ruby1.9

# Restart shell to refresh environment variables
> cinst 7zip.commandline

# Restart shell to refresh environment variables
> cinst ruby.devkit
```

* Install required gems

```
> gem install bundler 
```

* Run the bundle install to get required games

```
> bundle install --binstubs
```

* Run the build using rake.

```
> bundle exec rake
```

NOTE:
When installing Ruby DevKit it should automatically register itself with the
installed ruby version.  However, you may need to configure Ruby DevKit manually
by running the following.

```
# In Git bash
> echo "- C:/ruby193" >> /c/DevKit/config.yml
> ruby /c/DevKit/dk.rb install   

# Of if you are in in cmd
> echo "- C:/ruby193" >> c:\DevKit\config.yml
> ruby c:\DevKit\dk.rb install
```

If the above steps fail try the following 

```
> ruby c:\DevKit\dk.rb install --force
```

Another thing to try...

```
> cd c:\devkit
> chcp 1252
> ruby dk.rb init
```

One installed you should have access to the following tasks:

```
> bundle exec rake --tasks

rake compile          # Builds the solution
rake compile:build    # Builds the solution using the Build target
rake compile:clean    # Builds the solution using the Clean target
rake compile:rebuild  # Builds the solution using the Rebuild target
rake package          # Packages all nugets
rake publish          # Publish nuget packages to feed
rake publish:local    # Copy nuget packages to local path
rake restore          # Restores all nugets as per the packages.config files
rake test             # Run unit tests
```