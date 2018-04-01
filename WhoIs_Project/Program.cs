using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace DG_WhoIs_Project
{
    public class Program
    {
        static void Main(string[] args)
        {
            ReturnDomain: Console.WriteLine("Aramak istediğiniz Domain adını giriniz : ");
            string domainquerry = Console.ReadLine().ToLower().Trim().Replace(" ", string.Empty);
            if (domainquerry =="")
            {
                Console.WriteLine("Lütfen Domain için bir ad giriniz !");
                goto ReturnDomain;
            }

            Query(domainquerry);
            Console.WriteLine("\n");
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("Yeni bir sorgu yapmak istermisiniz E/H");
            string status = Console.ReadLine();

            if (status == "E" || status == "e")
            {
                goto ReturnDomain;
            }
            else
            {
                goto Break;
            }
            Break: Console.ReadLine();
            //END PROGRAM

            string GetDomainName(string url)
            {
                var doubleSlashesIndex = url.IndexOf("://");
                var start = doubleSlashesIndex != -1 ? doubleSlashesIndex + "://".Length : 0;
                var end = url.IndexOf("/", start);
                if (end == -1)
                    end = url.Length;
                string domainname = url.Substring(start, end - start);
                if (domainname.StartsWith("www."))
                    domainname = domainname.Substring("www.".Length);
                return domainname;
            }
            string GetDomainTld(string host)
            {
                var p = host.LastIndexOf(".");
                var domain = host.Substring(p + 1);
                return domain;
            }
            string getWhoisServer(string tld)
            {
                var whoisServers = new Dictionary<string, string>() {
                { "com","whois.verisign-grs.com" },
                { "net","whois.verisign-grs.com" },
                { "org","whois.publicinterestregistry.net" },
                { "info","whois.afilias.info" },
                { "biz","whois.neulevel.biz" },
                { "us","whois.nic.us" },
                { "uk","whois.nic.uk" },
                { "ca","whois.cira.ca" },
                { "tel","whois.nic.tel" },
                { "ie","whois.iedr.ie"},
                { "it","whois.nic.it" },
                { "li","whois.nic.li" },
                { "no","whois.norid.no" },
                { "cc","whois.nic.cc" },
                { "eu","whois.eu" },
                { "nu","whois.nic.nu" },
                { "au","whois.aunic.net" },
                { "de","whois.nic.de" },
                { "ws","whois.nic.ws" },
                { "sc","whois2.afilias-grs.net" },
                { "mobi","whois.dotmobiregistry.net" },
                { "pro","whois.registry.pro" },
                { "edu","whois.educause.net" },
                { "tv","whois.nic.tv" },
                { "travel","whois.nic.travel" },
                { "name","whois.nic.name" },
                { "in","whois.registry.in"},
                { "me","whois.nic.me" },
                { "at","whois.nic.at" },
                { "be","whois.dns.be" },
                { "cn","whois.cnnic.cn" },
                { "asia","whois.nic.asia" },
                { "ru","whois.ripn.ru" },
                { "ro","whois.rotld.ro"},
                { "aero","whois.aero" },
                { "fr","whois.nic.fr" },
                { "se","whois.nic.se" },
                { "nl","whois.sidn.nl" },
                { "nz","whois.srs.net.nz" },
                { "mx","whois.nic.mx" },
                { "tw","whois.apnic.net" },
                { "ch","whois.nic.ch" },
                { "hk","whois.hknic.net.hk" },
                { "ac","whois.nic.ac" },
                { "ae","whois.nic.ae" },
                { "af","whois.nic.af" },
                { "ag","whois.nic.ag" },
                { "al","whois.ripe.net" },
                { "am","whois.amnic.net" },
                { "as","whois.nic.as" },
                { "az","whois.ripe.net" },
                { "ba","whois.ripe.net" },
                { "bg","whois.register.bg" },
                { "bi","whois.nic.bi" },
                { "bj","www.nic.bj" },
                { "br","whois.nic.br" },
                { "bt","whois.netnames.net" },
                { "by","whois.ripe.net" },
                { "bz","whois.belizenic.bz" },
                { "cd","whois.nic.cd" },
                { "ck","whois.nic.ck" },
                { "co","whois.nic.co" },
                { "cl","nic.cl" },
                { "coop","whois.nic.coop" },
                { "cx","whois.nic.cx" },
                { "cy","whois.ripe.net" },
                { "cz","whois.nic.cz" },
                { "dk","whois.dk-hostmaster.dk" },
                { "dm","whois.nic.cx" },
                { "dz","whois.ripe.net" },
                { "ee","whois.eenet.ee" },
                { "eg","whois.ripe.net" },
                { "es","whois.nic.es" },
                { "fi","whois.ficora.fi" },
                { "fo","whois.ripe.net" },
                { "gb","whois.ripe.net" },
                { "ge","whois.ripe.net" },
                { "gl","whois.ripe.net" },
                { "gm","whois.ripe.net" },
                { "gov","whois.nic.gov" },
                { "gr","whois.ripe.net" },
                { "gs","whois.adamsnames.tc" },
                { "hm","whois.registry.hm" },
                { "hn","whois2.afilias-grs.net" },
                { "hr","whois.ripe.net" },
                { "hu","whois.ripe.net" },
                { "il","whois.isoc.org.il" },
                { "int","whois.isi.edu" },
                { "ig","vrx.net" },
                { "ir","whois.nic.ir" },
                { "is","whois.isnic.is" },
                { "je","whois.je" },
                { "jp","whois.jprs.jp" },
                { "kg","whois.domain.kg" },
                { "kr","whois.nic.or.kr" },
                { "la","whois2.afilias-grs.net" },
                { "lt","whois.domreg.lt" },
                { "lu","whois.restena.lu" },
                { "lv","whois.nic.lv" },
                { "ly","whois.lydomains.com" },
                { "ma","whois.iam.net.ma" },
                { "mc","whois.ripe.net" },
                { "md","whois.nic.md" },
                { "mil","whois.nic.mil" },
                { "mk","whois.ripe.net" },
                { "ms","whois.nic.ms" },
                { "mt","whois.ripe.net" },
                { "mu","whois.nic.mu" },
                { "my","whois.mynic.net.my" },
                { "nf","whois.nic.cx" },
                { "pl","whois.dns.pl" },
                { "pr","whois.nic.pr" },
                { "pt","whois.dns.pt" },
                { "sa","saudinic.net.sa" },
                { "sb","whois.nic.net.sb" },
                { "sg","whois.nic.net.sg" },
                { "sh","whois.nic.sh"},
                { "si","whois.arnes.si" },
                { "sk","whois.sk-nic.sk" },
                { "sm","whois.ripe.net" },
                { "st","whois.nic.st" },
                { "su","whois.ripn.net" },
                { "tc","whois.adamsnames.tc" },
                { "tf","whois.nic.tf" },
                { "th","whois.thnic.net" },
                { "tj","whois.nic.tj" },
                { "tk","whois.nic.tk" },
                { "tl","whois.domains.tl" },
                { "tm","whois.nic.tm" },
                { "tn","whois.ripe.net" },
                { "to","whois.tonic.to" },
                { "tp","whois.domains.tl" },
                { "tr","whois.nic.tr" },
                { "ua","whois.ripe.net" },
                { "uy","nic.uy" },
                { "uz","whois.cctld.uz" },
                { "va","whois.ripe.net" },
                { "vc","whois2.afilias-grs.net"},
                { "ve","whois.nic.ve"},
                { "vg","whois.adamsnames.tc"},
                { "yu","whois.ripe.net" },
                { "science","whois.iana.org" },
                { "xyz","whois.nic.xyz" },
                { "ist","whois.nic.istanbul" },
                { "istanbul","whois.nic.istanbul" },
                { "com.br","whois.centralnic.com" },
                { "com.cn","whois.centralnic.com" },
                { "com.eu","whois.centralnic.com"},
                { "com.gb","whois.centralnic.com"},
                { "net.gb","whois.centralnic.com"},
                { "com.qc","whois.centralnic.com" },
                { "com.hu","whois.centralnic.com" },
                { "com.no","whois.centralnic.com" },
                { "com.sa" ,"whois.centralnic.com" },
                { "com.se","whois.centralnic.com"},
                { "net.se","whois.centralnic.com"},
                { "com.uk","whois.centralnic.com"},
                { "net.uk","whois.centralnic.com"},
                { "uk.ac","whois.ja.net"},
                { "uk.gov","whois.ja.net"},
                { "com.us","whois.centralnic.com"},
                { "uy.com","whois.centralnic.com"},
                { "com.za","whois.centralnic.com"}};

                if (whoisServers.ContainsKey(tld))
                {
                    return whoisServers[tld];
                }
                return whoisServers[tld];
            }
            string Query(string domain)
            {
                string tr = ".tr";
                int sonuc = domainquerry.IndexOf(tr);
                try
                {
                    string domainname = GetDomainName(domain);
                    string whoisserver = getWhoisServer(GetDomainTld(domain));
                    TcpClient TCPC = new TcpClient(whoisserver, 43);
                    string strDomain = domainname + "\r\n";
                    byte[] arrDomain = Encoding.ASCII.GetBytes(strDomain);
                    Stream objStream = TCPC.GetStream();
                    objStream.Write(arrDomain, 0, strDomain.Length);
                    StreamReader objSR = new StreamReader(TCPC.GetStream(), Encoding.ASCII);
                    string icerik = objSR.ReadToEnd();
                    if (sonuc > 0)
                    {
                        GetResult_Com_Tr(icerik);
                    }
                    else
                    {
                        GetResult_Com(icerik);
                    }
                    TCPC.Close();
                    return icerik;
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
        }
        private static void GetResult_Com_Tr(string icerik)
        {
            string companyinfo = @"(?<=Registrant:)(?<Value>.*)(?=(?:Administrative Contact:))";
            RegexOptions optionscompany = RegexOptions.Singleline;
            foreach (Match m in Regex.Matches(icerik, companyinfo, optionscompany))
            {
                Console.WriteLine("SIRKET BILGILERI :\n {0}", m.Value);
            }

            string pattern = @"(?<=Additional Info:)(?<Value>.*)(?=(?:))";
            RegexOptions optionsdomain = RegexOptions.Singleline;

            foreach (Match m in Regex.Matches(icerik, pattern, optionsdomain))
            {
                Console.WriteLine("DOMAIN BILGILERI :\n {0}", m.Value);
            }

        }
        public static void GetResult_Com(string icerik)
        {
            if (!string.IsNullOrEmpty(icerik))
            {
                Regex regex = new Regex(pattern: @"(?<domain_name>.[^\n]*)\n. *(?<registry_domain_ıd>.[^\n]*)\n. *(?<whois_server>.[^\n]*)\n. *(?<referrral_url>.[^\n]*)\n. *(?<updated_date>.[^\n]*)\n. *(?<creation_date>.[^\n]*)\n. *(?<expiration_date>.[^\n]*)\n. *(?<registrar>.[^\n]*)\n. *(?<registrar_iana_id>.[^\n]*)\n. *(?<registrar_email>.[^\n]*)\n.*");
                Match match = regex.Match(icerik);
                if (match != null)
                {
                    string domainName = match.Groups["domain_name"].Value;
                    string registrar = match.Groups["registry_domain_ıd"].Value;
                    string whoisServer = match.Groups["whois_server"].Value;
                    string referrralUrl = match.Groups["referrral_url"].Value;
                    string nameServer1 = match.Groups["Name Server"].Value;
                    string nameServer2 = match.Groups["Name Server"].Value;
                    string status = match.Groups["status"].Value;
                    string updatedDate = match.Groups["updated_date"].Value;
                    string creationDate = match.Groups["creation_date"].Value;
                    string expirationDate = match.Groups["expiration_date"].Value;

                    Console.WriteLine(string.Format("DOMAIN ADI :{0}\n\r", domainName));
                    Console.WriteLine(string.Format("KAYITLI DOMAIN ID :{0}\n\r", registrar));
                    Console.WriteLine(string.Format("SINIRLI KAYIT KAYDI :{0}\n\r", whoisServer));
                    Console.WriteLine(string.Format("GUNCELLEME TARIHI :{0}\n\r", updatedDate));
                    Console.WriteLine(string.Format("OLUSTURMA TARIHI :{0}\n\r", creationDate));
                    Console.WriteLine(string.Format("SON KULLANMA TARIHI :{0}\n\r", expirationDate));
                }
            }
        }
    }
}


