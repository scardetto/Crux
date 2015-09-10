require 'physique'

Physique::Solution.new do |s|
  s.file = 'src/Crux.sln'

  s.publish_nugets do |p|
    p.feed_url = ENV['NUGET_FEED_URL'] || 'https://www.myget.org/F/thirdwave/api/v2/package'
    p.symbols_feed_url = ENV['NUGET_SYMBOL_FEED_URL'] || 'https://nuget.symbolsource.org/MyGet/thirdwave'
    p.api_key = ENV['NUGET_API_KEY']
    p.with_metadata do |m|
      m.description = 'Common libraries for crux applications'
      m.authors = 'Robert Scaduto, Leo Hernandez'
    end
  end
end
