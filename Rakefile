require 'physique'

Physique::Solution.new do |s|
  s.file = 'src/Crux.sln'

  s.publish_nugets do |p|
    p.feed_url = ENV['NUGET_FEED_URL']
    p.symbols_feed_url = ENV['NUGET_SYMBOL_FEED_URL']
    p.api_key = ENV['NUGET_API_KEY']
    p.with_metadata do |m|
      m.description = 'Common libraries for crux applications'
      m.authors = 'Robert Scaduto, Leo Hernandez'
    end
  end
end
