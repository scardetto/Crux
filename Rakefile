require 'physique'

Physique::Solution.new do |s|
  s.file = 'src/Crux.sln'

  s.publish_nugets do |p|
    p.feed_url = 'https://www.nuget.org'
    p.symbols_feed_url = 'http://nuget.gw.symbolsource.org/Public/NuGet'
    p.api_key = ENV['NUGET_API_KEY']
    p.with_metadata do |m|
      m.description = 'Common libraries for crux applications'
      m.authors = 'Robert Scaduto, Leo Hernandez'
    end
  end
end
